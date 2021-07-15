using System;
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

            case "End":
                LoadEndScreen();

                break;
            default:
                break;
        }
    }

    private void LoadEndScreen()
    {
        SceneManager.LoadScene("End");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            myCanvas.EnableInteartableText(true);
            canInteract = true;
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
}