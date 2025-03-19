using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuickStart
{
    public class PlayerScript : NetworkBehaviour
    {
        public TMP_Text playerNameText;
        public GameObject floatingInfo;

        private Material playerMaterialClone;
        private Weapon activeWeapon;
        private float weaponCooldownTime;  

        [SyncVar(hook = nameof(OnNameChanged))]
        public string playerName;

        [SyncVar(hook = nameof(OnColorChanged))]
        public Color playerColor = Color.white;

        private UIStuff uiStuff;
        public bool isCurrentScene;

        void OnNameChanged(string _Old, string _New)
        {
            playerNameText.text = playerName;
        }

        void OnColorChanged(Color _Old, Color _New)
        {
            /*playerNameText.color = _New;
            playerMaterialClone = new Material(GetComponentsInChildren<Renderer>().material);
            playerMaterialClone.color = _New;
            GetComponent<Renderer>().material = playerMaterialClone;*/
        }

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
            
            floatingInfo.transform.localPosition = new Vector3(0, -0.3f, 0.6f);
            floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            string name = "Player" + Random.Range(100, 999);
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            CmdSetupPlayer(name, color);
            sceneScript.playerScript = this;
        }

        [Command]
        public void CmdSetupPlayer(string _name, Color _col)
        {
            //player info sent to server, then server updates sync vars which handles it on all clients
            playerName = _name;
            playerColor = _col;
            //uiStuff.statusText = $"{playerName} joined.";
        }
    
        [TargetRpc] // This runs only on the specific client
        public void SetReadyUpButtonPlayer(NetworkConnectionToClient target, GameObject lobbyPlayer)
        {
            Debug.Log("Received from server: " + name);
            FindAnyObjectByType<ReadyUp>().lobbyPlayer = lobbyPlayer.GetComponent<NetworkedLobbyPlayer>();
        }

        void Update()
        {
            if (!isLocalPlayer)
            {
                // make non-local players run this
                floatingInfo.transform.LookAt(Camera.main.transform);
                return;
            }

            float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
            float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

            transform.Rotate(0, moveX, 0);
            transform.Translate(0, 0, moveZ);

            if (Input.GetAxis("Mouse ScrollWheel") > 0) //Fire2 is mouse 2nd click and left alt
            {
                selectedWeaponLocal += 1;

                if (selectedWeaponLocal > weaponArray.Length) 
                    selectedWeaponLocal = 1; 

                CmdChangeActiveWeapon(selectedWeaponLocal);
            }
            
            if (Input.GetButtonDown("Fire1") ) //Fire1 is mouse 1st click
            {
                if (activeWeapon && Time.time > weaponCooldownTime && activeWeapon.weaponAmmo > 0)
                {
                    weaponCooldownTime = Time.time + activeWeapon.weaponCooldown;
                    activeWeapon.weaponAmmo -= 1;
                    //uiStuff.UIAmmo(activeWeapon.weaponAmmo);
                    CmdShootRay();
                }
            }
        }
        
        [Command]
        void CmdShootRay()
        {
            RpcFireWeapon();
        }

        [ClientRpc]
        void RpcFireWeapon()
        {
            //bulletAudio.Play(); muzzleflash  etc
            GameObject bullet = Instantiate(activeWeapon.weaponBullet, activeWeapon.weaponFirePosition.position, activeWeapon.weaponFirePosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * activeWeapon.weaponSpeed;
            Destroy(bullet, activeWeapon.weaponLife);
        }
        
        private SceneScript sceneScript;

        void Awake()
        {
            //allow all players to run this
            sceneScript = GameObject.FindObjectOfType<SceneScript>();
            // disable all weapons
            foreach (var item in weaponArray)
                if (item != null)
                    item.SetActive(false);
            
            //allows all players to run this
            sceneScript = GameObject.Find("GameManager").GetComponent<SceneReference>().sceneScript;
            
            if (selectedWeaponLocal < weaponArray.Length && weaponArray[selectedWeaponLocal] != null)
            {
                activeWeapon = weaponArray[selectedWeaponLocal].GetComponent<Weapon>();
                //uiStuff.UIAmmo(activeWeapon.weaponAmmo);
            }
        }
        
        [Command]
        public void CmdSendPlayerMessage()
        {
            if (sceneScript) 
                uiStuff.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
        }
        
        private int selectedWeaponLocal = 1;
        public GameObject[] weaponArray;

        [SyncVar(hook = nameof(OnWeaponChanged))]
        public int activeWeaponSynced = 1;

        void OnWeaponChanged(int _Old, int _New)
        {
            // disable old weapon
            // in range and not null
            if (0 < _Old && _Old < weaponArray.Length && weaponArray[_Old] != null)
                weaponArray[_Old].SetActive(false);
    
            // enable new weapon
            // in range and not null
            if (0 < _New && _New < weaponArray.Length && weaponArray[_New] != null)
            {
                weaponArray[_New].SetActive(true);
                activeWeapon = weaponArray[activeWeaponSynced].GetComponent<Weapon>();
                if (isLocalPlayer)
                   uiStuff.UIAmmo(activeWeapon.weaponAmmo);
            }
        }

        [Command]
        public void CmdChangeActiveWeapon(int newIndex)
        {
            activeWeaponSynced = newIndex;
        }

        public void DisableShooting()
        {
            if (SceneManager.GetActiveScene("Lobby"))
            {
                isCurrentScene = P
            }
        }
    }
}
