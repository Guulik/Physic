using UnityEngine;
using TMPro;

public class Ballistic : MonoBehaviour
{
    public float height;
    public float permanentAcc, momentAcc,momentSpeed;
    private float distance, time, outputTime, outputSpeed, angle;
    private const float g = 9.81f;
    private bool isFinished = false;
    private bool isLanded = false;
    public TextMeshProUGUI outputText;
    
    public TMP_InputField heightInput;
    public TMP_InputField momentAccInput;
    public TMP_InputField permanentAccInput;
    public TMP_InputField speedInput;
    public TMP_InputField angleInput;
    
    
    private float vertMove, horizontalMove;
    private float speed, horizontalSpeed, verticalSpeed; 
    private Vector3 prevPosition = new Vector3(0f,0f,0f);
    

    void physCalc()
    {   
        angle = Mathf.Clamp(angle, 0f, 90f);
        if (permanentAcc == 0 && momentAcc == 0)
        {
            horizontalSpeed = momentSpeed;
            verticalSpeed += -g *Time.fixedDeltaTime;
            
            horizontalMove = horizontalSpeed * time * Time.fixedDeltaTime;
            vertMove = verticalSpeed  * time * Time.fixedDeltaTime;
        }
        else if (permanentAcc == 0 && momentSpeed == 0)
        {
            if (time <= .5f)
            {
                horizontalSpeed += momentAcc * Mathf.Cos(Mathf.Deg2Rad * angle)  * Time.fixedDeltaTime;
                verticalSpeed += (momentAcc * Mathf.Sin(Mathf.Deg2Rad * angle)- g) * Time.fixedDeltaTime;
            }
            horizontalMove = horizontalSpeed * time  * Time.fixedDeltaTime;
            vertMove = (verticalSpeed * time- g*time*time/2) * Time.fixedDeltaTime;
        }   
        else if (momentAcc == 0 && momentSpeed == 0)
        {
            horizontalSpeed += permanentAcc * Mathf.Cos(Mathf.Deg2Rad * angle)* Time.fixedDeltaTime;
            verticalSpeed += (permanentAcc * Mathf.Sin(Mathf.Deg2Rad * angle) - g) * Time.fixedDeltaTime;
            
            horizontalMove = horizontalSpeed  * time  * Time.fixedDeltaTime;
            vertMove = (verticalSpeed*time - g* time * time /2) * Time.fixedDeltaTime;
            
        }
        else setDefault();
        
        speed = Mathf.Sqrt(verticalSpeed*verticalSpeed + horizontalSpeed*horizontalSpeed);
        distance+= Vector3.Distance(transform.position, prevPosition);
        prevPosition = transform.position;


        if (transform.position.y <= -0.1f)
        {
            if (!isLanded)
            {
                outputSpeed = speed;
                isLanded = true;
            }
            vertMove = 0f;
            //isFinished = true;
        }
    }
    

    private void Start()
    {
        Time.timeScale = 0f;
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        
        physCalc();
        
        
        transform.position += new Vector3(0f, vertMove,horizontalMove);
    }

    private void Update()
    {
        
        outputText.text = string.Format(
                                        "\nРасстояние: {0:f3}" +
                                        "\nДальность полёта: {1:f3}" +
                                        "\nСкорость в приземлении: {2:f3}"+
                                        "\nСредняя скорость: {3:f3}"+
                                        "\n--------"+
                                        "\nВремя: {4:f3}", 
             distance, transform.position.z, outputSpeed, distance/time, time);
        
        if (Time.timeScale == 0f)
        {
            height = heightInput.text is "" or "-" ? 0 : float.Parse(heightInput.text);
            momentSpeed = speedInput.text is "" or "-" ? 0 : float.Parse(speedInput.text);
            angle = angleInput.text is "" or "-" ? 0 : float.Parse(angleInput.text);
            permanentAcc = permanentAccInput.text is "" or "-" ? 0 : float.Parse(permanentAccInput.text);
            momentAcc = momentAccInput.text is "" or "-" ? 0 : float.Parse(momentAccInput.text);
            if (!isFinished)
                setDefault();
        }
    }
    void setDefault()
    {
        transform.position =  new Vector3(0f, height, 0f);
        prevPosition = new Vector3(0f, height, 0f);
    }

}


