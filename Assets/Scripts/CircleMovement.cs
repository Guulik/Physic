using UnityEngine;
using TMPro;

public class CircleMovement : MonoBehaviour
{
    public float radius, Frequency;
    private float angularSpeed, linearSpeed, distance, RotationsCount, passedTime, angle, timer;

    public TextMeshProUGUI outputText;
    public TMP_InputField RadiusInput;
    public TMP_InputField FrequencyInput;
   
    private float prevRadius, prevFrequency, deltaAngle;

    void Start()
    {
        Time.timeScale = 0f;
    }
    void physCalc() //расчеты физ. величин (угловая скорость, линейная и т.д
    {
        angularSpeed = 2 * Mathf.PI * Frequency;
        linearSpeed = 2 * Mathf.PI * radius  * Frequency;
        RotationsCount = Frequency * passedTime;
        distance = 2 * Mathf.PI * radius * RotationsCount;
    }
    void FixedUpdate()
    {
        physCalc();
        passedTime += Time.fixedDeltaTime; //счетчик времени
        transform.position += linearSpeed * transform.forward  * Time.fixedDeltaTime;//наш танк движется всегда вперед,
        transform.rotation = Quaternion.Euler(0f,angle,0f);//и постоянно поворачивает. Поэтому он движется по кругу
        deltaAngle = angularSpeed * Mathf.Rad2Deg * Time.fixedDeltaTime; //угол поворота
        angle += deltaAngle; //угол вращения, относительно оси Y
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            readInputs();
            setDefault();
        }
        //вывод
        outputText.text = string.Format("Число оборотов: {0:f3}" + 
           "\nУгловая скорость: {1:f3}" +
           "\nУгол поворота: {2:f3}" +
           "\nПройденный путь: {3:f3}" +
           "\nЛинейная скорость: {4:f3}" +
           "\nx: {5:f3}" +
           "\nz: {6:f3}"+
           "\n--------"+
           "\nВремя: {7:f3}",
           RotationsCount, angularSpeed, deltaAngle, distance, linearSpeed, transform.position.x, transform.position.z,passedTime);
        
        
    }
    private void readInputs()
    {
        radius = RadiusInput.text == "" || RadiusInput.text == "-" ? 0 : float.Parse(RadiusInput.text);
        Frequency = FrequencyInput.text == "" || FrequencyInput.text == "-" ? 0 : float.Parse(FrequencyInput.text);
       
    }
    void setDefault()
    { 
        passedTime = 0f; //обнуляем таймер
       transform.rotation = Quaternion.Euler(0f, 0f, 0f);
       transform.position = new Vector3(-radius, 0f, 0f); //ставим в левую точку окружности, потому что
       //положительный угол поворота = поворот направо
       angle = 0f;
       
    }
}


