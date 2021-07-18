using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; set; }

    public GameObject visualPatternObject;
    public GameObject interactText, narrationBox;
    public TextMeshProUGUI objectiveText, timer, endDisplayTimer;
    public GameObject gamePanel, endPanel;

    public List<Image> keys;

    private void Awake()
    {
        Instance = this;
    }

    public void EnableCubePuzzle(bool value)
    {
        visualPatternObject.SetActive(value);
    }

    public void EnableInteartableText(bool value)
    {
        interactText.SetActive(value);
    }

    private int counter = 0;
    private bool countTime;

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

    public void SetObjectiveText(string objText)
    {
        objectiveText.text = objText;
    }

    private float currentTime;

    public void EnableTimer()
    {
        if (!countTime)
        {
            currentTime = 0;
        }
        countTime = true;
    }

    private void Update()
    {
        if (countTime)
        {
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timer.text = time.ToString(@"mm\:ss\:fff");
        }
    }

    public void StopTimer()
    {
        countTime = false;
        EndText();
    }

    public void EndText()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        endDisplayTimer.text = timer.text;
    }

}