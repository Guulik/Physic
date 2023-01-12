using UnityEngine;
using TMPro;

public class Punch1 : MonoBehaviour
{
    private Rigidbody rb1, rb2;
    private float mass1, mass2;
    private float speed1, speed2;
    private float xPos1,xPos2;
    private GameObject obj1,obj2;
    
    public TMP_InputField mass1Input, mass2Input;
    public TMP_InputField speed1Input, speed2Input;
    public TMP_InputField posInput1;
    public TMP_InputField posInput2;
    private void Start()
    {
        Time.timeScale = 0f;
    
        
        obj1 = GameObject.Find("Object 1");
        obj2 = GameObject.Find("Object 2");

        rb1 = obj1.GetComponent<Rigidbody>();
        rb2 = obj2.GetComponent<Rigidbody>();
    }
    private void Update()
    {

        if (Time.timeScale == 0f)
        {
            mass1 = mass1Input.text is "" or "-" ? 10 : float.Parse(mass1Input.text);
            mass2 = mass2Input.text is "" or "-" ? 10 : float.Parse(mass2Input.text);
            speed1 = speed1Input.text is "" or "-" ? 5 : float.Parse(speed1Input.text);
            speed2 = speed2Input.text is "" or "-" ? 5 : float.Parse(speed2Input.text);
            xPos1 = posInput1.text is "" or "-" ? 0 : float.Parse(posInput1.text);
            xPos2 = posInput2.text is "" or "-" ? 0 : float.Parse(posInput2.text);
            setDefault();
        }
        
    }
    private void setDefault()
    {
        rb1.mass = mass1;
        rb2.mass = mass2;
        rb1.velocity = transform.forward * speed1;
        rb2.velocity = -transform.forward * speed2;
        obj1.transform.position = new Vector3(xPos1,1f,-8f);
        obj2.transform.position = new Vector3(xPos2,1f,7f);
    }
}


