using System.Collections.Generic;
using UnityEngine;


public class PolygonManager : MonoBehaviour
{
    public List<GameObject> prefabs = new();

    private List<GameObject> lenses;
    private GameObject box;
    System.Random random = new();


    public class Box
    {
        public BoundsInt bounds;

        public Box(Vector3Int location, Vector3Int size)
        {
            bounds = new BoundsInt(location, size);
        }

        public static bool Intersect(Box a, Box b)
        {
            return !((a.bounds.position.x >= (b.bounds.position.x + b.bounds.size.x)) ||
                     ((a.bounds.position.x + a.bounds.size.x) <= b.bounds.position.x)
                     || (a.bounds.position.y >= (b.bounds.position.y + b.bounds.size.y)) ||
                     ((a.bounds.position.y + a.bounds.size.y) <= b.bounds.position.y)
                     || (a.bounds.position.z >= (b.bounds.position.z + b.bounds.size.z)) ||
                     ((a.bounds.position.z + a.bounds.size.z) <= b.bounds.position.z));
        }
    }

    public List<Box> BoxesPlaced;

    public void PlaceBoxes()
    {
        for (int i = 0; i < random.Next(7, 12); i++)
        {
            Vector3Int location = new Vector3Int(
                random.Next(-16,16),
                1,
                random.Next(-6, 26)
            );
            Vector3Int roomSize = new Vector3Int(
                random.Next(2, 5),
                1,
                3
            );


            bool add = true;
            Box newBox = new Box(location, roomSize);
            Box buffer = new Box(location + new Vector3Int(-1, 0, -1), roomSize + new Vector3Int(2, 0, 2));

            foreach (var box in BoxesPlaced)
            {
                if (Box.Intersect(box, buffer))
                {
                    add = false;
                    break;
                }
            }

            if (add)
            {
                BoxesPlaced.Add(newBox);

                placeBox(newBox.bounds.position,newBox.bounds.size);
            }
        }
    }

    void placeBox(Vector3Int location, Vector3Int size)
    {
        int prefab_choice = random.Next(0, prefabs.Count);
        box = Instantiate(
            prefabs[prefab_choice],
            location,
            Quaternion.Euler(0f, random.Next(0, 360), 0f),
            transform
        );
        Vector3 scale = box.GetComponent<Transform>().localScale ;
        scale = new Vector3(scale.x+size.x-1, scale.y,scale.z);
        box.GetComponent<Transform>().localScale = scale;
        
       

    }

    void Start()
    {
        BoxesPlaced = new List<Box>();
    }
}

    

  