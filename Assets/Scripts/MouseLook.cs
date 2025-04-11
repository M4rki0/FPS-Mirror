using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1500f;
    public Transform playerBody;
    private float xRotation = 0f;

    public bool dontLook;

    void Start()
    {
        dontLook = true;
        if (dontLook) mouseSensitivity = 0;
    }

    void Update()
    {
        if (GameManager.Instance.localPlayer.GetComponent<QuickStart.PlayerScript>().isPlayerInGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            dontLook = false;
            mouseSensitivity = 1500f;
        }

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
    
    /*public void SetLookEnabled(bool isEnabled)
    {
        dontLook = !isEnabled;
        Cursor.lockState = isEnabled ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isEnabled;
    }*/
}