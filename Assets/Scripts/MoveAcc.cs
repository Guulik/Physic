using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class MoveAcc : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public TMP_InputField axInput;
    public TMP_InputField azInput;
    public TMP_InputField bxInput;
    public TMP_InputField bzInput;
    public TMP_InputField t1Input;
    public TMP_InputField t2Input;
    public TMP_InputField t3Input;

    private float _speedX, _speedZ ;
    

    [SerializeField] private float accelerationX , accelerationZ ;
    [SerializeField] private float acAcX, acAcZ;
    [SerializeField] private float t1, t2, t3;
    private float _timePassed, _distance , _resultSpeed, _resultAcceleration;
    private Vector3 _stPoint;
    private bool _isParsed,_isOutputed;
    private void Start()
    {
         axInput.text = "0";
         azInput.text = "0";
         bxInput.text = "0";
         bzInput.text = "0";
         t1Input.text = "0";
         t2Input.text = "0";
         t3Input.text = "0";
         Time.timeScale = 0f;
    }
    private string setOutputText()
    {
        return            $"Distance: {_distance:f3}" + 
                          $"\nSpeed X:\n {_speedX:f3}" + $"\nSpeed Z:\n {_speedZ:f3}" + $"\nresSpeed:\n {_resultSpeed:f3}" + 
                          $"\nAcceleration X:\n {accelerationX:f3}" + $"\nAcceleration Z:\n {accelerationZ:f3}" + 
                          $"\nresAcceleration:\n {_resultAcceleration:f3}"+
                          $"\nTime: {_timePassed:f3}" + 
                          $"\nx:\n {transform.position.x:f3}" + $"\nz:\n {transform.position.z:f3}";
    }
    
    private void Update()
    {
        if(!_isOutputed)outputText.text = "Here will be the output at t3\n \n \n"+
                          $"current time: {_timePassed:f3}";

        if ((float.Parse(axInput.text) != accelerationX || float.Parse(azInput.text) != accelerationZ) && !_isParsed)
        {
            accelerationX = axInput.text is "" or "-" ? 0 : float.Parse(axInput.text);
            accelerationZ = azInput.text is "" or "-" ? 0 : float.Parse(azInput.text);
            _isParsed = true;
        }
    
        acAcX = bxInput.text is "" or "-" ? 0 : float.Parse(bxInput.text);
        acAcZ = bzInput.text is "" or "-" ? 0 : float.Parse(bzInput.text);
        

        t1 = t1Input.text is "" or "-" || float.Parse(t1Input.text) <= 0
            ? 0f : float.Parse(t1Input.text);
        t2 = t2Input.text is "" or "-" || float.Parse(t2Input.text) <= 0
            ? 15f : float.Parse(t2Input.text);
        t3 = t3Input.text is "" or "-" || float.Parse(t3Input.text) <= 0
            ? 10f : float.Parse(t3Input.text);
    }
    private void FixedUpdate()
    {
        _timePassed += Time.fixedDeltaTime;

        _stPoint = transform.position;

        if (Math.Abs(_timePassed - t3) < Time.fixedDeltaTime / 2)
        {
            outputText.text = setOutputText();
            _isOutputed = true;
        }
        if (Math.Abs(_timePassed - t2) < Time.fixedDeltaTime/2) Time.timeScale = 0f;

        if (_timePassed > t1)
        {
            _speedX += accelerationX * Time.fixedDeltaTime;             
            _speedZ += accelerationZ * Time.fixedDeltaTime;
            accelerationX += acAcX * Time.fixedDeltaTime;
            accelerationZ += acAcZ * Time.fixedDeltaTime;
        }

        _resultSpeed = Mathf.Sqrt(_speedX * _speedX + _speedZ * _speedZ);
        _resultAcceleration = Mathf.Sqrt(accelerationX * accelerationX + accelerationZ * accelerationZ);

        transform.position += new Vector3(_speedX*Time.fixedDeltaTime, 0, _speedZ*Time.fixedDeltaTime);

        _distance += Mathf.Sqrt(Mathf.Pow(transform.position.z - _stPoint.z, 2)
            + Mathf.Pow(transform.position.x - _stPoint.x, 2)); 
    }
}
