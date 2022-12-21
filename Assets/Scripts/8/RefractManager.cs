using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Linq;
using JetBrains.Annotations;
using static System.Random;

public class RefractManager : MonoBehaviour
{
    public List<GameObject> lenses_prefabs = new();
    
    [CanBeNull] public TMP_Dropdown Lensescount;
    [CanBeNull] public TMP_Dropdown LenseIndex;
    [CanBeNull] public Slider LenseIndex_Slider;
    [CanBeNull] public TMP_Text slider_text;
    
    [Range(1f,3f)]private float s;
    [SerializeField] [CanBeNull] private Slider SInput;
    
    private RefractAdjust refractAdjust;

    private List<GameObject> lenses;    
    [CanBeNull] private TMP_Dropdown _nIn;
    private GameObject lense;
    System.Random random = new();

    RefractAdjust curLense;

    class Lense {
        public BoundsInt bounds;

        public Lense(Vector3Int location, Vector3Int size) {
            bounds = new BoundsInt(location, size);
        }

        public static bool Intersect(Lense a, Lense b) {
            return !((a.bounds.position.x >= (b.bounds.position.x + b.bounds.size.x)) || ((a.bounds.position.x + a.bounds.size.x) <= b.bounds.position.x)
                || (a.bounds.position.y >= (b.bounds.position.y + b.bounds.size.y)) || ((a.bounds.position.y + a.bounds.size.y) <= b.bounds.position.y)
                || (a.bounds.position.z >= (b.bounds.position.z + b.bounds.size.z)) || ((a.bounds.position.z + a.bounds.size.z) <= b.bounds.position.z));
        }
    }

    private List<Lense> LensesBounded;

    public void PlaceLenses()
    {
        for (int i = 0; i < random.Next(4, 8); i++)
        {
            Vector3Int location = new Vector3Int(
                random.Next(-10,10),
                0,
                random.Next(-10,10)
            );
            Vector3Int roomSize = new Vector3Int(
                2,
                2,
                2
            );


            bool add = true;
            Lense newLense = new Lense(location, roomSize);
            Lense buffer = new Lense(location + new Vector3Int(-1, 0, -1), roomSize + new Vector3Int(2, 0, 2));

            foreach (var lense in LensesBounded)
            {
                if (Lense.Intersect(lense, buffer))
                {
                    add = false;
                    break;
                }
            }

            if (add)
            {
                LensesBounded.Add(newLense);
                
                placeLense(newLense.bounds.position);
            }
        }
    }
    void placeLense(Vector3Int location)
    {
        int prefab_choice = random.Next(0, lenses_prefabs.Count);
        lense = Instantiate(
            lenses_prefabs[prefab_choice],
            location,
            Quaternion.Euler(0f,random.Next(0,360),0f),
            transform
        );
        if (prefab_choice != 0)
            lenses.Add(lense);
        
    }
    void Start()
    {
       
        lenses = new List<GameObject>();
        LensesBounded = new List<Lense>();
        if (GameObject.Find("Dropdown") != null){_nIn = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();}
        
    }

    

    void Update()
    {
        curLense = SelectLense();
        
        if (curLense != null)
        {
            curLense.s = SInput.value;
        }
    }
    
    public void IndexChange(){ applyN();}

    public RefractAdjust SelectLense()
    {
        if (lenses.Count != 0)
        {
                //DropDown Selection
            if (LenseIndex != null)
            {
                return lenses[LenseIndex.value].GetComponent<RefractAdjust>();
            }

            //slider Selection
            if (LenseIndex_Slider != null && slider_text != null)
            {
                int indexSlide = (int)LenseIndex_Slider.value;
                slider_text.text = string.Format("{0}", LenseIndex_Slider.value);
                return lenses[indexSlide].GetComponent<RefractAdjust>();
            }
        }

        return null;
    }
    
    public void applyN()
    {
        curLense = SelectLense();
        curLense.n = curLense.refractMaterials[_nIn.options[_nIn.value].text];
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
                    new Vector3(0f, 0f, 2 + 2 * i),
                    Quaternion.identity,
                    transform
                );
                lenses.Add(lense);
            }  
            
        }
        
    }
}
