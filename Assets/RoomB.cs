using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomB : MonoBehaviour
{
    public static RoomB Instance { get; set; }
    public List<GameObject> objectsToDestroy, keysObtained;
    private int counter = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void KeyObtained()
    {
        counter++;
        if (counter >= 2)
        {
            foreach (var item in objectsToDestroy)
            {
                Destroy(item);
            }
        }
    }
}
