using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Punch2 : MonoBehaviour
{
    
    public TMP_InputField massInput;
    public TMP_InputField speedInput;
    public TMP_InputField angleInput;

    
    private bool isFinished;
    private Rigidbody rb;
    private PolygonManager _MapGenerator;
    
    [Range(0f,360f)] private float angle;
    private float mass;
    private float speed;
    
    private void Start()
    {
        Time.timeScale = 0f;
        
        rb = GetComponent<Rigidbody>();

        _MapGenerator = GameObject.Find("Polygon Creator").GetComponent<PolygonManager>();
        _MapGenerator.BoxesPlaced = new List<PolygonManager.Box>();
        _MapGenerator.PlaceBoxes();
    }
    private void Update()
    {

        if (Time.timeScale == 0f)
        {
            mass = massInput.text is "" or "-" ? 10 : float.Parse(massInput.text);
            speed = speedInput.text is "" or "-" ? 5 : float.Parse(speedInput.text);
            angle = angleInput.text is "" or "-" ? 0 : float.Parse(angleInput.text);
            setDefault();
        }
        
    }
    //перекраска блока "финиш" в синий цвет
    private void OnTriggerEnter(Collider finish)
    {
        if (finish.transform.tag == "Finish" && !isFinished)
        {
            isFinished = true;
            
            Renderer _renderer = finish.GetComponent<Renderer>();
            _renderer.material.color = Color.blue;
        }
    } 
    private void setDefault()
    {
        rb.mass = mass;
        rb.velocity = transform.forward * speed;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }
}


