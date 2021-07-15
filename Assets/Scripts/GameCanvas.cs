using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; set; }

    public GameObject visualPatternObject;
    public GameObject interactText, narrationBox;
    public TextMeshProUGUI objectiveText;

    public List<Image> keys;

    private void Awake()
    {
        Instance = this;
    }

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
        keys[counter].color = new Color(1, 1, 1, 1);
        counter++;        
    }

    public void EnableMap()
    {
        objectiveText.text = "Right click for map, but it will take you back to checkpoint.";
    }


    public void ShowNarrationText(bool value, string textToDisplay)
    {
        narrationBox.SetActive(value);
        var nb = narrationBox.GetComponent<AdjustNarrationText>();
        nb.SetUpText(textToDisplay);
    }
}
