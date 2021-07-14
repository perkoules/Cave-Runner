using UnityEngine;

public class VisualPattern : MonoBehaviour
{
    public MeshRenderer[] cubes;
    public Material white;
    public GameObject rockToDestroy;

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
            Destroy(rockToDestroy);
            return true;
        }
        else
        {
            ChangeMaterial();
            return false;
        }
    }
}