using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameObject visualPatternObject;
    public GameObject interactText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI countKeys;

    public void EnableCubePuzzle()
    {
        visualPatternObject.SetActive(true);
    }

    public void EnableInteartableText(bool value)
    {
        interactText.SetActive(value);
    }
    int counter = 0;
    public void SetKeyValue()
    {
        counter++;
        countKeys.text = counter.ToString(); 
    }
}
