using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FrictionAdjust : MonoBehaviour
{
    private BoxCollider coll;

    private float friction;

    public TMP_InputField frictionInput;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        friction = frictionInput.text is "" or "-" ? 0.2f : float.Parse(frictionInput.text);
        coll.material.dynamicFriction = friction;
        coll.material.staticFriction = friction;

    }
}
