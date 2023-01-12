using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    public GameObject mainObj;
    private float rad;

    void Update()
    {
        mainObj = GameObject.Find("Main object");
        
        SpiralMovement circle = mainObj.GetComponent<SpiralMovement>();
        rad = circle.radius;
        transform.position = rad == 0? new Vector3(0f, transform.position.y, 0f) 
            : new Vector3(-8+5*Mathf.Sqrt(Mathf.Abs(rad)), 1f, 0f); 
            
        //transform.position = mainObj.transform.position.y == 0? new Vector3(-8f, transform.position.y, 0f) 
          //  : new Vector3(-5*Mathf.Sqrt(Mathf.Abs(rad))-8f, 0f, 0f); 
    }
}