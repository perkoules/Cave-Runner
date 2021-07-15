using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public GameCanvas myCanvas;
    public List<Transform> checkpoints;
    public Material grey;
    private bool canInteract;


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
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.up, 2);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Key"))
                {
                    Destroy(hitCollider.transform.gameObject);
                    myCanvas.SetKeyValue();
                }
                if (hitCollider.CompareTag("KeyB"))
                {
                    Destroy(hitCollider.transform.gameObject);
                    myCanvas.SetKeyValue();
                    RoomB.Instance.KeyObtained();
                }
            }
        }
    }

}