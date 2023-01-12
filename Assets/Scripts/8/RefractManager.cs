using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

using JetBrains.Annotations;


public class RefractManager : MonoBehaviour
{
    public List<GameObject> lenses_prefabs = new();
    
    [CanBeNull] public TMP_Dropdown Lensescount;
    [CanBeNull] public Slider LenseIndex_Slider;
    [CanBeNull] public TMP_Text slider_text;
    
    [Range(1f,3f)]private float s;
    [SerializeField] [CanBeNull] private Slider SInput;
    [SerializeField] [CanBeNull] private Slider NInput;
    
    private RefractAdjust refractAdjust;

    public List<GameObject> lenses;    
    [CanBeNull] private TMP_Dropdown _nIn;
    private GameObject lense;
    System.Random random = new();

    RefractAdjust curLense;

    private void start()
    {
        LenseIndex_Slider.minValue = 0;
    }
    public void applyS()
    {
        if (lenses.Count != 0)
        {
            curLense = SelectLense();
            curLense.s = SInput.value;
        }
    }
    public void applyN()
    {
        if (lenses.Count != 0)
        {
            curLense = SelectLense();
            curLense.n = NInput.value;
        } 
    }
    public void IndexChange(){slider_text.text = string.Format("{0}", LenseIndex_Slider.value); }

    public RefractAdjust SelectLense()
    {
        if (lenses.Count != 0)
        {
            //slider Selection
            if (LenseIndex_Slider != null && slider_text != null)
            {
                int indexSlide = (int)LenseIndex_Slider.value-1;
                return lenses[indexSlide].GetComponent<RefractAdjust>();
            }
        }

        return null;
    }
    
    public void generateLenses()
    {
        
        int number = Lensescount.value;
        
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
                    lenses_prefabs[random.Next(0, lenses_prefabs.Count)],
                    new Vector3(2 + 2 * i, 0f, 0f),
                    Quaternion.identity,
                    transform
                );
                lenses.Add(lense);
            }
        }
        
        LenseIndex_Slider.maxValue = Lensescount.value;
    }
}
