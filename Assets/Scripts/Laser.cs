using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Laser : MonoBehaviour
{
    [SerializeField] private float mass = 1f;
    [SerializeField] private float time;
    [SerializeField] private float force;

    
    public TMP_InputField squaureInput;
    public TMP_InputField refractionInput;
    public TMP_InputField angleInput;
    
    private Rigidbody _rb;
    private float timer = 0f;
    //private bool flag;

    private bool isFinished;
    private Rigidbody rb;
    [Range(0f,360f)] private float angle;


    private void Start()
    {
        Time.timeScale = 0f;
        _rb = GetComponent<Rigidbody>();

        timer -= time;
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            angle = angleInput.text is "" or "-" ? 0 : float.Parse(angleInput.text);
            angle = angleInput.text is "" or "-" ? 0 : float.Parse(angleInput.text);
            angle = angleInput.text is "" or "-" ? 0 : float.Parse(angleInput.text);


        }
        timer += Time.deltaTime;
        _rb.AddForce(transform.up * force);
    }

    public void setPos1()
    {
        transform.position = new Vector3(1.5f, 0.5f, 0f);
        transform.rotation = Quaternion.Euler(0f,0f,90f);
    }
    /**
  * returns:
  *  normalized Vector3 refracted by passing from one medium to another in a realistic manner according to Snell's Law
  *
  * parameters:
  *  RI1 - the refractive index of the first medium
  *  RI2 - the refractive index of the second medium
  *  surfNorm - the normal of the interface between the two mediums (for example the normal returned by a raycast)
  *  incident - the incoming Vector3 to be refracted
  *
  * usage example (laser pointed from a medium with RI roughly equal to air through a medium with RI roughly equal to water):
  *  Vector3 laserRefracted = Refract(1.0f, 1.33f, waterPointNorm, laserForward);
*/
    public static Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident)
    {
        surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
        incident.Normalize();

        return (RI1/RI2 * Vector3.Cross(surfNorm, 
            Vector3.Cross(-surfNorm, incident)) - surfNorm
            * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident)*(RI1/RI2*RI1/RI2),
                Vector3.Cross(surfNorm, incident)))).normalized;
    }
}

