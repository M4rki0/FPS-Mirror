using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyUp : MonoBehaviour
{
    public bool isReady;

    public TMP_Text buttonText;

    public void Start()
    {
        isReady = false;
        buttonText.text = "Not Ready";
    }

    public void ReadyUpButton()
    {
        isReady = !isReady;

        if (isReady)
        {
            buttonText.text = "Ready";
        }
        else
        {
            buttonText.text = "Not Ready";
        }

        Debug.Log("Player is " + (isReady ? "Ready" : "Not Ready"));
    }
}
