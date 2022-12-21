using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Movement : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public TMP_InputField xspeed_input;
    public TMP_InputField zspeed_input;
    public TMP_InputField xacc_input;
    public TMP_InputField zacc_input;
    public TMP_InputField zStart_input;
    public TMP_InputField xStart_input;
    public TMP_InputField time_input;

    private float speedX=0f, speedZ=0f, startX, startZ ;
    
    private float accelerationX = 0f, accelerationZ = 0f, Time_limit = 100f;
    private float TimePassed = 0f, distance = 0f, result_Speed;
    private Vector3 st_point;

    void Start()
    {
        xspeed_input.text = "0";
        zspeed_input.text = "0";
        xacc_input.text = "0";
        zacc_input.text = "0";
        time_input.text = "0";
        Time.timeScale = 0f;

    }


    void Update()
    {
        //вывод значений на экран
        outputText.text = string.Format("Distance: {0:f3}" +
            "\nx speed: {1:f3}" +
            "\nz speed: {2:f3}" +
            "\nSpeed: {3:f3}" +
            "\nTime: {4:f3}" +
            "\nx: {5:f3}" +
            "\nz: {6:f3}" ,
            distance, speedX, speedZ, result_Speed, TimePassed, transform.position.x, transform.position.z);

        //по нажатию кнопки R считывется значение скорости
        //значения считываются только в том случае, если поле не пустое и в нём не знак минуса
        if (Input.GetKeyDown("r"))
        {
            Time.timeScale = 1f;
            speedX = xspeed_input.text == "" || xspeed_input.text == "-" ? 0 : float.Parse(xspeed_input.text);
            speedZ = zspeed_input.text == "" || zspeed_input.text == "-" ? 0 : float.Parse(zspeed_input.text);
            startX = xStart_input.text is "" or "-" ? 0 : float.Parse(xStart_input.text);
            startZ = zStart_input.text is "" or "-" ? 0 : float.Parse(zStart_input.text);
            setDefault();
        }
        accelerationX = xacc_input.text == "" || xacc_input.text == "-" ? 0 : float.Parse(xacc_input.text);
        accelerationZ = zacc_input.text == "" || zacc_input.text == "-" ? 0 : float.Parse(zacc_input.text);
        Time_limit = time_input.text == "" || time_input.text == "-" || float.Parse(time_input.text) <= 0
            ? 1000 : float.Parse(time_input.text);

        if (TimePassed >= Time_limit)// таймер времени
            Time.timeScale = 0f;
       
    }
    private void FixedUpdate()//здесь физика
    {
        TimePassed += Time.fixedDeltaTime;

        st_point = transform.position;//стартовая позиция

        speedX += accelerationX * Time.fixedDeltaTime;
        speedZ += accelerationZ * Time.fixedDeltaTime;

        result_Speed = Mathf.Sqrt(speedX * speedX + speedZ * speedZ);//результирующий вектор скорости

        transform.position += new Vector3(speedX*Time.fixedDeltaTime, 0, speedZ*Time.fixedDeltaTime);

        distance += Mathf.Sqrt(Mathf.Pow(transform.position.z - st_point.z, 2)
            + Mathf.Pow(transform.position.x - st_point.x, 2)); //длина вектора пройденного пути
    }

    private void setDefault()
    {
        transform.position= new Vector3(startX, 25f, startZ);
    }
}
