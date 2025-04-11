using System.Collections;
using System.Collections.Generic;
using Mirror;
using QuickStart;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIStuff : MonoBehaviour
{
    public TMP_Text canvasAmmoText;
    public TMP_Text canvasStatusText;

    public TMP_Text canvasHealthText;
    //[SyncVar(hook = nameof(OnStatusTextChanged))]
    public string statusText;
    public PlayerScript playerScript;

    void OnStatusTextChanged(string _Old, string _New)
    {
        //called from sync var hook, to update info on screen for all players
        canvasStatusText.text = statusText;
    }

    public void UIHealth(int _hValue)
    {
        canvasHealthText.text = "Health: " + _hValue;
    }
    
    public void UIAmmo(int _value)
    {
        canvasAmmoText.text = "Ammo: " + _value;
    }
}
