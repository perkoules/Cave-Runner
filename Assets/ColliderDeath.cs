using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDeath : MonoBehaviour
{
    public BoxCollider colliderToDestroy;

    private void OnDestroy()
    {
        Destroy(colliderToDestroy);
        GameCanvas.Instance.EnableInteartableText(false);
    }
}
