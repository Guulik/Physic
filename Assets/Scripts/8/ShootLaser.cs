using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShootLaser : MonoBehaviour
{
    [Range(-90f,90f)] private float angle;
    [Range(-7f,7f)]private float pos;
    [SerializeField] private Slider posInput;
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
        pos = posInput.value*7f;
        transform.rotation = Quaternion.Euler(0f, angle,0f);
        transform.position = new Vector3(pos, 0f, -1f);
    }
}
