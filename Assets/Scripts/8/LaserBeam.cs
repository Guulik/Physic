using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LaserBeam
{
    private Vector3 pos, dir;

    public GameObject laserObj;
    private LineRenderer laser;
    private List<Vector3> laserIndices = new List<Vector3>();

    private List<GameObject> _refractSurfaces = new ();
    [CanBeNull] private GameObject target = GameObject.FindWithTag("Target");
    [CanBeNull] private Particles _particles;
    

    [CanBeNull] RefractAdjust manager = GameObject.Find("Lenses").GetComponent<RefractAdjust>();
    [CanBeNull] GameObject LenseManager = GameObject.Find("Lenses");
    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        laser = new LineRenderer();
        laserObj = new GameObject();
        laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        laser = laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        laser.startWidth = laser.endWidth = 0.1f;
        laser.material = material;
        laser.startColor = laser.endColor = Color.yellow;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
                CheckHit(hit,dir,laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if (target != null)
        {
            Renderer target_renderer = target.GetComponent<Renderer>();
            _particles = target.GetComponent<Particles>();
        }

        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            if (target != null) unHittedEffect();

            CastRay(pos, dir, laser);
        }
        else if (hitInfo.collider.gameObject.tag == "Refract")
        {
            Vector3 pos = hitInfo.point;
            laserIndices.Add(pos);

            Vector3 newPos1 = new Vector3(Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.001f + pos.x,
                Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.001f + pos.y,
                Mathf.Abs(direction.z) / (direction.z + 0.0001f) * 0.001f + pos.z);


            RefractAdjust lense = hitInfo.collider.gameObject.GetComponent<RefractAdjust>();
            float n1 = 1f;
            float n2 = lense.n;
            
            Vector3 norm = hitInfo.normal;
            Vector3 incident = direction;

            Vector3 refractedVector = Refract(n1, n2, norm, incident);
            
            
            Ray ray1 = new Ray(newPos1, refractedVector);
            Vector3 newRayStartPos = ray1.GetPoint(2f);

            Ray ray2 = new Ray(newRayStartPos, -refractedVector);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 2f, 1))
            {
                laserIndices.Add(hit2.point);
            }
            if (target != null) unHittedEffect();

            
            UpdateLaser();

            Vector3 refractedVector2 = Refract(n2, n1, -hit2.normal, refractedVector);
            CastRay(hit2.point,refractedVector2,laser);
        }
        else if (hitInfo.collider.gameObject.tag == "Target")
        {
            if (_particles!= null) _particles.isHitted = true;
            
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {
            if (target != null) unHittedEffect();
            UpdateLaser();
        }
    }
    
    public static Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident)
    {
        surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
        incident.Normalize();

        return (RI1/RI2 * Vector3.Cross(surfNorm, 
                Vector3.Cross(-surfNorm, incident)) - surfNorm
            * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident)*(RI1/RI2*RI1/RI2),
                Vector3.Cross(surfNorm, incident)))).normalized;
    }

    public static Ray Refraction(float N1, float N2, Ray ray, RaycastHit hit)
    {
        float AngleBetweenFirstRayAndNormal = Vector3.Angle(ray.direction, hit.normal*-1);
        float AngleBetweenSecondRayAndNormal = 
            Mathf.Asin(Mathf.Clamp(Mathf.Sin(AngleBetweenFirstRayAndNormal*Mathf.PI/180)*N1/N2,
                -1, 1))*180/Mathf.PI;

        ray.direction = Quaternion.Euler(0, AngleBetweenSecondRayAndNormal
                                            * Mathf.Sign(Vector3.SignedAngle(ray.direction, hit.normal, Vector3.up)),
            0) * hit.normal*-1;
        return ray;
    }

    private void unHittedEffect()
    {
        Renderer target_renderer = target.GetComponent<Renderer>();
        _particles.isHitted = false;
    }
    
}
