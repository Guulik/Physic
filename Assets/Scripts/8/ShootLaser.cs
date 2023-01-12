using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShootLaser : MonoBehaviour
{
    [Range(-90f,270f)] private float angle;
    [SerializeField] private Slider angleInput;
    public Material Material; 
    LaserBeam beam;
    void Update()
    {
        if (beam != null)
        {
            Destroy(beam.laserObj);
        } 
        beam = new LaserBeam(transform.position, transform.forward, Material);

        angle = angleInput.value;

        transform.rotation = Quaternion.Euler(0f, angle+90f,0f);
   
    }
}
