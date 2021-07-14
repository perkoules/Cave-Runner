using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatueAttack : MonoBehaviour
{
    public float force = 20f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPos;


    private void Start()
    {
        InvokeRepeating("Fire", 5f, Random.Range(2f, 5f));
    }

    private void Fire()
    {
        GameObject go = Instantiate(projectilePrefab, projectileSpawnPos.position, projectileSpawnPos.rotation);
        go.transform.SetParent(null);
        go.GetComponent<Rigidbody>().AddForce(projectileSpawnPos.forward * force, ForceMode.Impulse);
        Destroy(go, 3f);
    }
}
