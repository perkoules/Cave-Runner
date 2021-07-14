using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameCanvas myCanvas;
    public List<Transform> checkpoints;
    public Material grey;
    private bool canInteract;

    private void Awake()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.tag;
        switch (otherTag)
        {
            case "Damage":
                GoToCheckpoint();
                break;

            case "Pattern":
                myCanvas.EnableCubePuzzle();
                break;

            case "Columns":
                other.GetComponent<MeshRenderer>().material = grey;
                bool res = other.GetComponentInParent<VisualPattern>().CheckTaskCompletion(other.transform.gameObject.name);
                if (!res)
                {
                    GoToCheckpoint();
                }
                break;

            case "Checkpoint":
                AddCheckpoint(other.transform);
                break;

            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            myCanvas.EnableInteartableText(true);
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            myCanvas.EnableInteartableText(false);
        }
    }

    private void AddCheckpoint(Transform tr)
    {
        if (!checkpoints.Contains(tr))
        {
            checkpoints.Add(tr);
        }
    }

    public void GoToCheckpoint()
    {
        var posToGo = checkpoints.Last().transform;
        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false;
        transform.position = posToGo.position;
        cc.enabled = true;
    }

    public void OnInteract()
    {
        if (canInteract)
        {
            Debug.Log("Bow obtained");
            //Obtain the bow
        }
    }
}