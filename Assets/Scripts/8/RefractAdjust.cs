using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RefractAdjust : MonoBehaviour
{
    public float n;
    public float s;
    
    public Dictionary<string, float> refractMaterials = new ()
    {
        { "Air", 1.0f },
        { "Glass", 1.5f },
        { "SuperMassiveCron", 1.7424f},
        { "ExtremeTest", 4f},
    };

    public void Start()
    {
        s = 1f;
        n = 2f;
    }

    public void Update()
    {
        transform.localScale = new Vector3(s,
            transform.localScale.y,
            transform.localScale.z);
        
    }
}
