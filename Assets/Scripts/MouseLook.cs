using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1500f;
    public Transform playerBody;
    private float xRotation = 0f;

    public bool dontLook;
    private bool _firstTimeSetup;
    public bool disabled;

    void Start()
    {
        dontLook = true;
        if (dontLook) mouseSensitivity = 0;
    }

    void Update()
    {
        if (disabled) return;
        if (GameManager.Instance.localPlayer == null) return;
        if (!GameManager.Instance.localPlayer.GetComponent<QuickStart.PlayerScript>().isPlayerInGame) return;
        if (!_firstTimeSetup) { FirstTime(); }
            
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player horizontally
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void FirstTime()
    {
        _firstTimeSetup = true;
        Cursor.lockState = CursorLockMode.Locked;
        dontLook = false;
        mouseSensitivity = 1500f;
    }
    
    /*public void SetLookEnabled(bool isEnabled)
    {
        dontLook = !isEnabled;
        Cursor.lockState = isEnabled ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isEnabled;
    }*/
}