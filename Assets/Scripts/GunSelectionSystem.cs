using Mirror;
using QuickStart;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectionSystem : MonoBehaviour
{
    public enum GunType { SMG=0, Shotgun=1, Sniper=2, AR=3 }

    private GunType selectedGun;
    private PerkSystem.PerkType selectedPerk;

    [Header("Gun Buttons")]
    public Button[] gunButtons; // Assign in Inspector (AR, SMG, Sniper, Shotgun)
    
    [Header("Perk Buttons")]
    public Button[] perkButtons; // Assign in Inspector (Teleport, AimAssist, x2Dmg, lessDmg)

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    public bool isSetupActive;
    public GamesList gamesList;

    // Method to be called by UI Buttons when selecting a gun

    void Start()
    {

        /*if (isSetupActive == true)
        {
            GameManager.Instance.localPlayer.GetComponent<GamesList>().canvas.enabled = true;
            GameManager.Instance.localPlayer.GetComponent<MouseLook>().mouseSensitivity = 0f;
        }*/

    }
    public void SelectGun(int gunIndex)
    {
        Debug.Log($"SelectGun() CALLED with index {gunIndex}");

        selectedGun = (GunType)gunIndex;
        Debug.Log($"SelectGun() - Selected: {selectedGun}");
        
        UpdateButtonHighlights(gunButtons, gunIndex);
    }



    public void SelectPerk(int perkIndex)
    {
        selectedPerk = (PerkSystem.PerkType)perkIndex;
        Debug.Log($"Perk selected: {selectedPerk}");

        UpdateButtonHighlights(perkButtons, perkIndex);
    }

    // Method to confirm selection and pass data to GameManager
    public void ConfirmSelection()
    {

        /*if (isSetupActive)
        {
            GameManager.Instance.localPlayer.GetComponent<GamesList>().canvas.enabled = false;
            GameManager.Instance.localPlayer.GetComponent<MouseLook>().mouseSensitivity = 1500f;
        }*/
        
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is NULL");
            return;
        }

        // Force set values to test if the issue is in selection updating
        //selectedGun = GunType.AR; // Replace with a gun you DIDN'T pick
        //selectedPerk = PerkType.lessDmg; // Replace with a perk you DIDN'T pick

       // Debug.Log($"ConfirmSelection() - Force-setting Gun: {selectedGun}, Perk: {selectedPerk}");

       Debug.Log($"Attempting set loadout: Gun = {selectedGun}, Perk = {selectedPerk}");
       GameManager.Instance.localPlayer.GetComponent<PlayerScript>().SetLocalLoadout(selectedGun, selectedPerk);
       
       Debug.Log("Gun selection has been confirmed");
       
       GameManager.Instance.localPlayer.GetComponent<PlayerScript>().EnterGame();

        //NetworkManager.singleton.StartClient();
    }
    
    /*public void ConfirmSelection()
    {
        Debug.Log($"ConfirmSelection() - Initial Values: Gun = {selectedGun}, Perk = {selectedPerk}");
    
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is NULL");
            return;
        }
    }*/

    // Helper method to update button colors
    private void UpdateButtonHighlights(Button[] buttons, int selectedIndex)
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            ColorBlock colors = buttons[i].colors;
            colors.normalColor = (i == selectedIndex) ? selectedColor : normalColor;
            buttons[i].colors = colors; 
        }
    }
}

