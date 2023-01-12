using UnityEngine;

public class Punch1 : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform pointOfImpact;
    [SerializeField] private float mass;
    [SerializeField] private float speed;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        
        transform.LookAt(pointOfImpact);

        rb.velocity = transform.forward * speed;
        
    }
    
}


