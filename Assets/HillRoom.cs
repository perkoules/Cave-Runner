using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillRoom : MonoBehaviour
{
    public static HillRoom Instance { get; set; }
    public List<GameObject> objectsToDestroy, keysObtained;
    private int counter = 0;

    private void Awake()
    {
        Instance = this;
    }
}
