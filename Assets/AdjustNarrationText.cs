using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdjustNarrationText : MonoBehaviour
{
    public TextMeshProUGUI narrationText;


    public void SetUpText(string text)
    {
        narrationText.text = text;
    }
}
