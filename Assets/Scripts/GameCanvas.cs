using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameObject visualPatternObject;
    public GameObject interactText;

    public void EnableCubePuzzle()
    {
        visualPatternObject.SetActive(true);
    }

    public void EnableInteartableText(bool value)
    {
        interactText.SetActive(value);
    }
}
