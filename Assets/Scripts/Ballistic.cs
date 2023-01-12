using UnityEngine;
using TMPro;

public class Ballistic : MonoBehaviour
{
    public float height;
    public float permanentAcc, momentAcc,momentSpeed;
    private float distance,  time,  angle;
    private float land_dist, outputTime, outputLandSpeed, outputDistance;
    private const float g = 9.81f;
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
    }
    

    private void Start()
    {
        Time.timeScale = 0f;
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        
        physCalc();
        LandCheck();

        transform.position += new Vector3(0f, vertMove,horizontalMove);
    }

    private void LandCheck()
    {
        if (!isLanded)
        {
            outputDistance = distance;
            outputLandSpeed = speed;
            outputTime = time;
        }
        if (transform.position.y <= -0.1f)
        {
            isLanded = true;
            land_dist = transform.position.z;
            vertMove = 0f;
        }

    }
    private void Update()
    {
        
        outputText.text = string.Format(
                                        "\nПуть: {0:f3}" +
                                        "\nДальность полёта: {1:f3}" +
                                        "\nСкорость в приземлении: {2:f3}"+
                                        "\nСредняя скорость до приземления: {3:f3}"+
                                        "\n--------"+
                                        "\nВремя: {4:f3}", 
             distance, land_dist, outputLandSpeed, outputDistance/outputTime, time);
        
        if (Time.timeScale == 0f)
        {
            height = heightInput.text is "" or "-" ? 0 : float.Parse(heightInput.text);
            momentSpeed = speedInput.text is "" or "-" ? 0 : float.Parse(speedInput.text);
            angle = angleInput.text is "" or "-" ? 0 : float.Parse(angleInput.text);
            permanentAcc = permanentAccInput.text is "" or "-" ? 0 : float.Parse(permanentAccInput.text);
            momentAcc = momentAccInput.text is "" or "-" ? 0 : float.Parse(momentAccInput.text);
            setDefault();
        }
    }
    void setDefault()
    {
        transform.position =  new Vector3(0f, height, 0f);
        prevPosition = new Vector3(0f, height, 0f);
    }

}


