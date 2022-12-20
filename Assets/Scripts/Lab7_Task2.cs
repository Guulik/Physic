using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Lab7_Task2 : MonoBehaviour
{
    [SerializeField] private float mass = 1f;
    [FormerlySerializedAs("force")] [SerializeField] private float speed;
    [SerializeField] private float Startspeed;
    [SerializeField] private float A;


    public TMP_InputField massInput;
    [FormerlySerializedAs("forceInput")] public TMP_InputField speedInput;
    public TMP_InputField time;
    [FormerlySerializedAs("angleInput")] public TMP_InputField Af;
    
    private Rigidbody _rb;
    private float timer = 0f, passedTime = 0f;
    //private bool flag;

    private Rigidbody rb;

   
    private void Start()
    {
        Time.timeScale = 0f;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            mass = massInput.text is "" or "-" ? 1 : float.Parse(massInput.text);
            speed = speedInput.text is "" or "-" ? 0 : float.Parse(speedInput.text);
            A = Af.text is "" or "-" ? 0 : float.Parse(Af.text);
            timer = time.text is "" or "-" ? 1f : float.Parse(time.text);
            
            _rb.mass = mass;
            passedTime = 0f;
        }
    }

    private void FixedUpdate()
    {
        passedTime += Time.fixedDeltaTime;
        if (passedTime >= timer )
        {

            Startspeed = speed;
       
            
            if (passedTime <= timer + 0.2f)
            {
                _rb.AddForce(-Vector3.right * Startspeed, ForceMode.Force);
            }
            else
            {
                speed += A * Time.fixedDeltaTime;
                _rb.AddForce(-Vector3.right * speed, ForceMode.Force);
            }
        }
    }

   
}
