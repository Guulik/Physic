using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public bool isHitted;

    private Renderer _renderer; 
    public Material _DefaultMaterial;
    public Material _HittedMaterial;
    [CanBeNull] public GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        isHitted = false;
        _renderer = gameObject.GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitted)
        {
            //StartCoroutine(waiter());
            //GameObject particle = Instantiate(particles, transform.position, Quaternion.Euler(-90f,0f,0f), transform);
            //Destroy(particle,.5f);
            _renderer.material = _HittedMaterial;
        }
        else
        {
            _renderer.material = _DefaultMaterial;
        }
    }
}
