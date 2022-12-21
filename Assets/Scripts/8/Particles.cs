using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public bool isHitted;

    private Renderer _renderer; 
    public Material _DefaultMaterial;
    public Material _HittedMaterial;
    public GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        isHitted = false;
        _renderer = gameObject.GetComponent<Renderer>();
        //StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitted)
        {
            //StartCoroutine(waiter());
            GameObject particle = Instantiate(particles, transform.position, Quaternion.Euler(-90f,0f,0f), transform);
            Destroy(particle,.5f);
            _renderer.material = _HittedMaterial;
        }
        else
        {
            _renderer.material = _DefaultMaterial;
        }
    }
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(0.25f);
        
    }
}
