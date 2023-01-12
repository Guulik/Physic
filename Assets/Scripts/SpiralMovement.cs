using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class SpiralMovement : MonoBehaviour
{
    public float radius;
    private float Speed;
    private float A_acceleration,B_acceleration;
    private float angularSpeed, linearSpeed, freq = 0.5f, distance, RotationsCount, passedTime, angle;
    private float timer = 5f, t1 = 15f;

    public TextMeshProUGUI outputText;
    public TMP_InputField RadiusInput;
    public TMP_InputField SpeedInput;
    public TMP_InputField A_accelereationInput;
    public TMP_InputField B_accelerationInput;
    public TMP_InputField Timer; 
    public TMP_InputField Timer1;
    
    private float prevRadius, prevFrequency,prevA, deltaAngle, vertSpeed, acceleration=0f;
    private Vector3 outputPosition = new Vector3(0f,0f,0f);
    private Vector3 prevPosition = new Vector3(0f,0f,0f);
    private Vector3 PosDifference = new Vector3(0f,0f,0f);


    void physCalc()
    {
        acceleration = A_acceleration + B_acceleration * passedTime;
        vertSpeed = Speed+acceleration*passedTime;
        
        angularSpeed = 2 * Mathf.PI * freq;
        linearSpeed = 2 * Mathf.PI * radius  * freq;
        RotationsCount = freq * passedTime;


        PosDifference += transform.position-prevPosition;
        prevPosition = transform.position;
        distance = PosDifference.magnitude;
        
        if (passedTime > t1)
        {
            outputPosition = transform.position;
        }
    }
    void setDefault()
    {
        if (prevRadius != radius)
        {

            passedTime = 0f;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.position = new Vector3(-radius, 0f, 0f);

            angle = 0f;
        }
        prevRadius = radius;
    }

    private void Start()
    {
        Time.timeScale = 0f;
        outputPosition = transform.position;
    }

    void FixedUpdate()
    {
        setDefault();
        passedTime += Time.fixedDeltaTime;
        if (passedTime >= timer)
        {
            physCalc();
            transform.position += (transform.forward * linearSpeed + transform.up*vertSpeed) * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            deltaAngle = angularSpeed * Mathf.Rad2Deg * Time.fixedDeltaTime;
            angle += deltaAngle;
            
        }
    }

    private void Update()
    {
        
        outputText.text = string.Format(
                                        "\nРасстояние: {1:f3}" +
                                        "\nВерт. скорость: {2:f3}" +
                                        "\nУскорение: {3:f3}" +
                                        "\nx: {4:f3}" +
                                        "\ny: {5:f3}"+
                                        "\nz: {6:f3}"+
                                        "\n--------"+
                                        "\nВремя: {7:f3}", 
            RotationsCount, distance, vertSpeed, acceleration, outputPosition.x, outputPosition.y, outputPosition.z, passedTime);
        
        timer = Timer.text is "" or "-" ? 0 : float.Parse(Timer.text);
        t1 = Timer1.text is "" or "-" ? 10 : float.Parse(Timer1.text);
        radius = RadiusInput.text is "" or "-" ? 0 : float.Parse(RadiusInput.text);
        if (Time.timeScale == 0f)
        {
            Speed = SpeedInput.text is "" or "-" ? 0 : float.Parse(SpeedInput.text);
            A_acceleration = A_accelereationInput.text is "" or "-" ? 0 : float.Parse(A_accelereationInput.text);
            B_acceleration = B_accelerationInput.text is "" or "-" ? 0 : float.Parse(B_accelerationInput.text);
        }
    }
    public void ReadInputs()
    {
        timer = Timer.text is "" or "-" ? 0 : float.Parse(Timer.text);
        t1 = Timer1.text is "" or "-" ? 10 : float.Parse(Timer1.text);
        radius = RadiusInput.text is "" or "-" ? 0 : float.Parse(RadiusInput.text);
        Speed = SpeedInput.text is "" or "-" ? 0 : float.Parse(SpeedInput.text);
        A_acceleration = A_accelereationInput.text is "" or "-" ? 0 : float.Parse(A_accelereationInput.text);
        B_acceleration = B_accelerationInput.text is "" or "-" ? 0 : float.Parse(B_accelerationInput.text);
        
    }

}


