using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Lab7_Task1 : MonoBehaviour
{
    [SerializeField] private float mass = 1f;
    [SerializeField] private float force, A;

    
    public TMP_InputField massInput;
    public TMP_InputField forceInput;
    public TMP_InputField time;
    [FormerlySerializedAs("angleInput")] public TMP_InputField Af;
    
    private Rigidbody _rb;
    private float timer;
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
            force = forceInput.text is "" or "-" ? 0 : float.Parse(forceInput.text);
            A = Af.text is "" or "-" ? 0 : float.Parse(Af.text);
            timer = time.text is "" or "-" ? 1f : float.Parse(time.text);
            
            _rb.mass = mass;
        }
    }

    private void FixedUpdate()
    {
        
        if (Time.fixedTime >= timer)
        {
            force += A * Time.fixedDeltaTime;
            _rb.AddForce(-Vector3.right * force, ForceMode.Force);
        }
    }
}
