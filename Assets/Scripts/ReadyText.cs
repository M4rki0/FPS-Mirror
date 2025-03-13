using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyText : MonoBehaviour
{
    public ReadyUp _readyUp;
    public TMP_Text otherText;
    public bool hasButtonBeenClicked;

    public void Start()
    {
        otherText.text = "Not Ready";
    }

    /*public void Update()
    {
        if (otherText.text = "Not Ready")
        {
            otherText.text = Color.red;
        }
        else
        {
            otherText.text = Color.green;
        }
    }

    public void ReadyUpButton()
    {
        _readyUp.GetComponent<Button>();
    }

    public void ButtonClick()
    {
        if (hasButtonBeenClicked)
        {
            _readyUp.GetComponent<Button>().onClick;
        }
    }*/
    
}
