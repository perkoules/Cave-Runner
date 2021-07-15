using System.Collections.Generic;
using UnityEngine;

public class VisualPattern : MonoBehaviour
{
    public MeshRenderer[] cubes;
    public Material white;
    public List<GameObject> objectsToDestroy;

    public void ChangeMaterial()
    {
        foreach (var item in cubes)
        {
            item.material = white;
        }
    }

    public bool CheckTaskCompletion(string cubeName)
    {
        if (cubeName == "Correct")
        {
            foreach (var item in cubes)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in objectsToDestroy)
            {
                Destroy(item);
            }
            return true;
        }
        else
        {
            ChangeMaterial();
            return false;
        }
    }
}