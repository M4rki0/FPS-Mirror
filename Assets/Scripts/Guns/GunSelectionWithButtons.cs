using UnityEngine;
using UnityEngine.UI;

public class GunSelectionWithButtons : MonoBehaviour
{
    public enum GunType { AR, SMG, Sniper, Shotgun }

    [Header("UI References - Guns")]
    public Button arButton;
    public Button smgButton;
    public Button sniperButton;
    public Button shotgunButton;

    [Header("UI References - Perks")]
    public Button doubleDamageButton;
    public Button halfDamageButton;
    public Button teleportationButton;
    public Button aimAssistButton;

    [Header("Other UI")]
    public Button startGameButton;

    [Header("Selected Options")]
    public GunType selectedGun;
    public PerkSystem.PerkType selectedPerk;

    private void Start()
    {
        // Assign button listeners for gun selection
        arButton.onClick.AddListener(() => SelectGun(GunType.AR));
        smgButton.onClick.AddListener(() => SelectGun(GunType.SMG));
        sniperButton.onClick.AddListener(() => SelectGun(GunType.Sniper));
        shotgunButton.onClick.AddListener(() => SelectGun(GunType.Shotgun));

        // Assign button listeners for perk selection
        doubleDamageButton.onClick.AddListener(() => SelectPerk(PerkSystem.PerkType.DoubleDamage));
        halfDamageButton.onClick.AddListener(() => SelectPerk(PerkSystem.PerkType.HalfDamage));
        teleportationButton.onClick.AddListener(() => SelectPerk(PerkSystem.PerkType.Teleportation));
        aimAssistButton.onClick.AddListener(() => SelectPerk(PerkSystem.PerkType.AimAssist));

        // Assign the start game button listener
        startGameButton.onClick.AddListener(StartGame);
    }

    private void SelectGun(GunType gun)
    {
        selectedGun = gun;
        Debug.Log($"Gun Selected: {gun}");
        HighlightSelectedGun(gun);
    }

    private void SelectPerk(PerkSystem.PerkType perk)
    {
        selectedPerk = perk;
        Debug.Log($"Perk Selected: {perk}");
        HighlightSelectedPerk(perk);
    }

    private void HighlightSelectedGun(GunType gun)
    {
        // Add your visual feedback logic here (e.g., change button color or size)
        ResetGunButtonHighlights();
        switch (gun)
        {
            case GunType.AR: arButton.GetComponent<Image>().color = Color.green; break;
            case GunType.SMG: smgButton.GetComponent<Image>().color = Color.green; break;
            case GunType.Sniper: sniperButton.GetComponent<Image>().color = Color.green; break;
            case GunType.Shotgun: shotgunButton.GetComponent<Image>().color = Color.green; break;
        }
    }

    private void HighlightSelectedPerk(PerkSystem.PerkType perk)
    {
        // Add your visual feedback logic here (e.g., change button color or size)
        ResetPerkButtonHighlights();
        switch (perk)
        {
            case PerkSystem.PerkType.DoubleDamage: doubleDamageButton.GetComponent<Image>().color = Color.green; break;
            case PerkSystem.PerkType.HalfDamage: halfDamageButton.GetComponent<Image>().color = Color.green; break;
            case PerkSystem.PerkType.Teleportation: teleportationButton.GetComponent<Image>().color = Color.green; break;
            case PerkSystem.PerkType.AimAssist: aimAssistButton.GetComponent<Image>().color = Color.green; break;
        }
    }

    private void ResetGunButtonHighlights()
    {
        arButton.GetComponent<Image>().color = Color.white;
        smgButton.GetComponent<Image>().color = Color.white;
        sniperButton.GetComponent<Image>().color = Color.white;
        shotgunButton.GetComponent<Image>().color = Color.white;
    }

    private void ResetPerkButtonHighlights()
    {
        doubleDamageButton.GetComponent<Image>().color = Color.white;
        halfDamageButton.GetComponent<Image>().color = Color.white;
        teleportationButton.GetComponent<Image>().color = Color.white;
        aimAssistButton.GetComponent<Image>().color = Color.white;
    }

    private void StartGame()
    {
        Debug.Log($"Starting game with Gun: {selectedGun}, Perk: {selectedPerk}");
        GameManager1.Instance.SetPlayerLoadout(selectedGun, selectedPerk);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MyScene");
    }
}