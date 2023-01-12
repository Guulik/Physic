using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Movement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private TMP_InputField xspeed_input;
    [SerializeField] private TMP_InputField zspeed_input;
    [SerializeField] private TMP_InputField xacc_input;
    [SerializeField] private TMP_InputField zacc_input;
    [SerializeField] private TMP_InputField zStart_input;
    [SerializeField] private TMP_InputField xStart_input;
    [SerializeField] private TMP_InputField time_input;

    private static float speedX, speedZ, startX, startZ ;
    private static float accelerationX, accelerationZ, Time_limit;
    
    private float TimePassed, distance, result_Speed;
   
    void Start()
    {
        Time.timeScale = 0f;
    }


    void Update()
    {

        outputText.text = string.Format("Distance: {0:f3}" +
            "\nx speed: {1:f3}" +
            "\ny speed: {2:f3}" +
            "\nSpeed: {3:f3}" +
            "\nTime: {4:f3}" +
            "\nx: {5:f3}" +
            "\ny: {6:f3}" ,
            distance, speedX, speedZ, result_Speed, TimePassed, transform.position.x, transform.position.z);

        
        if (Time.timeScale == 0f)
        {
            readInputs();
            setDefault();
        }

        if (TimePassed >= Time_limit)
            Time.timeScale = 0f;
       
    }

    private void FixedUpdate()
    {
        TimePassed += Time.fixedDeltaTime;

        speedX += accelerationX * Time.fixedDeltaTime;
        speedZ += accelerationZ * Time.fixedDeltaTime;

        result_Speed = Mathf.Sqrt(speedX * speedX + speedZ * speedZ);

        transform.position += new Vector3(speedX*Time.fixedDeltaTime, 0, speedZ*Time.fixedDeltaTime);
      
        
        distance = result_Speed * TimePassed;
    }

    private void readInputs()
    {
  
        speedX = xspeed_input.text is "" or "-" ? 0 : float.Parse(xspeed_input.text);
        speedZ = zspeed_input.text is "" or "-" ? 0 : float.Parse(zspeed_input.text);
        startX = xStart_input.text is "" or "-" ? 0 : float.Parse(xStart_input.text);
        startZ = zStart_input.text is "" or "-" ? 0 : float.Parse(zStart_input.text);
        accelerationX = xacc_input.text is "" or "-" ? 0 : float.Parse(xacc_input.text);
        accelerationZ = zacc_input.text is "" or "-" ? 0 : float.Parse(zacc_input.text);
        Time_limit = time_input.text is "" or "-" || float.Parse(time_input.text) <= 0
            ? 60 : float.Parse(time_input.text);
    }
    private void setDefault()
    {
        distance = 0f;
        TimePassed = 0f;
        transform.position= new Vector3(startX, 0f, startZ);
    }
}
