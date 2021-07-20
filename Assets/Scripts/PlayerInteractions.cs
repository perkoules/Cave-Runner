using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    public GameCanvas myCanvas;
    public List<Transform> checkpoints;
    public Material grey;
    private bool canInteract;
    private bool canDetonate;
    public GameObject killingSphere;
    private bool canShowMap;

    public static PlayerInteractions Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.tag;
        switch (otherTag)
        {
            case "Damage":
                GoToCheckpoint();
                break;

            case "DeathPit":
                GoToCheckpoint();
                break;

            case "Pattern":
                myCanvas.EnableCubePuzzle(true);
                break;

            case "Columns":
                other.GetComponent<MeshRenderer>().material = grey;
                bool res = other.GetComponentInParent<VisualPattern>().CheckTaskCompletion(other.transform.gameObject.name);
                if (!res)
                {
                    GoToCheckpoint();
                }
                else
                {
                    myCanvas.SetObjectiveText("");
                }
                break;

            case "Checkpoint":
                AddCheckpoint(other);
                break;

            case "Narration":
                DisplayNarration(other);
                break;

            case "End":
                LoadEndScreen();
                break;

            case "Time":
                EnableTime();

                break;

            default:
                break;
        }
    }

    private void EnableTime()
    {
        myCanvas.EnableTimer();
    }

    private void AddCheckpoint(Collider other)
    {
        myCanvas.SetObjectiveText(other.GetComponent<SetObjective>().ObjectText);
        if (!checkpoints.Contains(other.transform))
        {
            checkpoints.Add(other.transform);
        }
    }

    private void DisplayNarration(Collider other)
    {
        var n = other.GetComponent<NarrationText>();
        myCanvas.ShowNarrationText(true, n.NarText);
    }

    private void LoadEndScreen()
    {
        Color col = new Color(1, 1, 1, 1);
        if (myCanvas.keys.All(img => img.color == col))
        {
            myCanvas.StopTimer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.transform.childCount == 1)
            {
                myCanvas.EnableInteartableText(true);
                canInteract = true;
            }
        }
        if (other.CompareTag("Rumble"))
        {
            canDetonate = true;
            if (killingSphere != null)
            {
                killingSphere.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            myCanvas.EnableInteartableText(false);
        }
        if (other.CompareTag("Rumble"))
        {
            canDetonate = false;
            if (killingSphere != null)
            {
                killingSphere.SetActive(false);
            }
        }
        if (other.CompareTag("Narration"))
        {
            myCanvas.ShowNarrationText(false, "");
        }
    }

    public void GoToCheckpoint()
    {
        var posToGo = checkpoints.Last().transform;
        //var posToGo = checkpoints[0].transform;
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
                if (hitCollider.CompareTag("KeyHill"))
                {
                    Destroy(hitCollider.transform.gameObject);
                    myCanvas.SetKeyValue();
                    HillRoom.Instance.KeyObtained();
                }
                if (hitCollider.CompareTag("KeyMap"))
                {
                    Destroy(hitCollider.transform.gameObject);
                    myCanvas.SetKeyValue();
                    myCanvas.EnableMap();
                }
            }
        }
    }

    public void OnAttack()
    {
        if (canDetonate)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.up, 15);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    hitCollider.transform.gameObject.GetComponent<Enemy>().EnemeGotHit();
                }
            }
        }

        if (canShowMap)
        {
            StartCoroutine(ShowMapForSeconds());
        }
    }

    private IEnumerator ShowMapForSeconds()
    {
        Maze.Instance.EnableMap(true);
        yield return new WaitForSeconds(1f);
        Maze.Instance.EnableMap(false);
        yield return new WaitForSeconds(1f);
        Maze.Instance.BackToPlayerView();
        GoToCheckpoint();
    }

    public bool CanShowMap
    {
        get
        {
            return canShowMap;
        }
        set
        {
            canShowMap = value;
        }
    }

    public void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}