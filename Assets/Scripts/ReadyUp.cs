using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyUp : MonoBehaviour
{
    public bool isReady;

    public TMP_Text buttonText;

    public TMP_Text textText;

    public void Start()
    {
        isReady = false;
        buttonText.text = "Not Ready";
        textText.text = "(Not Ready)";
        textText.color  = Color.red;
    }

    public void ReadyUpButton()
    {
        isReady = !isReady;

        if (isReady)
        {
            buttonText.text = "Ready";
            textText.text = "(Ready)";
            textText.color = Color.green;
        }
        else
        {
            buttonText.text = "Not Ready";
            textText.text = "(Not Ready)";
            textText.color  = Color.red;
        }

        Debug.Log("Player is " + (isReady ? "Ready" : "Not Ready"));
    }
}
