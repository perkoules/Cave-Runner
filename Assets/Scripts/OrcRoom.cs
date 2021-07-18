using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrcRoom : MonoBehaviour
{
    public List<GameObject> objectsToDestroy, enemies;

    public Transform initialPosition;
    public static OrcRoom Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }
    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemies.Remove(enemyToRemove);
        enemies.TrimExcess();
        if(enemies.Count <= 0)
        {
            foreach (var item in objectsToDestroy)
            {
                Destroy(item);
            }
            Destroy(PlayerInteractions.Instance.killingSphere);
            GameCanvas.Instance.SetObjectiveText("");
        }
    }

    public void GoToInitialPosition()
    {
        foreach (var item in enemies)
        {
            item.transform.position = initialPosition.position;
        }
    }
}
