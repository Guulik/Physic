using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static System.Random;

public class RefractManager : MonoBehaviour
{
    public List<GameObject> lenses_prefabs = new();
    
    public TMP_Dropdown Lensescount;
    public TMP_Dropdown LenseIndex;

    [Range(1f,3f)]private float s;
    [SerializeField] private Slider SInput;
    
    private RefractAdjust refractAdjust;

    private List<GameObject> lenses = new();
    private TMP_Dropdown _nIn;
    private GameObject lense;
    System.Random rand = new System.Random(); 
    void Start()
    {
        lense = Instantiate(
            lenses_prefabs[rand.Next(0,2)],
            new Vector3(0f,0f,3+2),
            Quaternion.identity,
            transform
        );
        lenses.Add(lense);
        _nIn = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();


    }

    void Update()
    {
        
                refractAdjust = lenses[LenseIndex.value].GetComponent<RefractAdjust>();
                refractAdjust.s = SInput.value;
            
    }
    
    public void IndexChange(){ applyN();}
    public void applyN()
    {
        
        refractAdjust = lenses[LenseIndex.value].GetComponent<RefractAdjust>();
        refractAdjust.n = refractAdjust.refractMaterials[_nIn.options[_nIn.value].text];
    }
    public void generateLenses()
    {
        int number = Lensescount.value+1;
        
       

        if (number != lenses.Count)
        {
            for (int i = 0; i < lenses.Count; i++)
            {
                Destroy(lenses[i]);
            }
            lenses = new List<GameObject>();
            for (int i = 0; i<number;i++)
            {
                lense = Instantiate(
                    lenses_prefabs[rand.Next(0, 2)],
                    new Vector3(0f, 0f, 2 + 2 * i),
                    Quaternion.identity,
                    transform
                );
                lenses.Add(lense);
            }  
            
        }
        
    }
}
