using System;
using Mirror;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor.SceneManagement;

namespace QuickStart
{
    public class PlayerScript : NetworkBehaviour
    {
        public TMP_Text playerNameText;
        public GameObject floatingInfo;
        public Transform playerCameraPosition;

        private Material playerMaterialClone;
        public Weapon weapon;
        public AmmoManager ammoManager;
        private float weaponCooldownTime;  

        [SyncVar(hook = nameof(OnNameChanged))]
        public string playerName;

        [SyncVar(hook = nameof(OnColorChanged))]
        public Color playerColor = Color.white;

        private UIStuff uiStuff;
        public bool isCurrentScene;
        public bool canShoot;
        public bool isPlayerInGame;
        public float moveSpeed = 10;

        [SyncVar(hook = nameof(OnSelectedGunChanged))]
        public GunSelectionSystem.GunType selectedGun;
        public GameObject[] guns;

        private CharacterController _characterController;

        public void Start()
        {
            //Scene currentScene 
            //if ()
            _characterController = GetComponent<CharacterController>();
        }

        void OnNameChanged(string _Old, string _New)
        {
            playerName = _New;
            playerNameText.text = playerName;
        }

        void OnColorChanged(Color _Old, Color _New)
        {
            /*playerNameText.color = _New;
            playerMaterialClone = new Material(GetComponentsInChildren<Renderer>().material);
            playerMaterialClone.color = _New;
            GetComponent<Renderer>().material = playerMaterialClone;*/
        }

        void OnSelectedGunChanged(GunSelectionSystem.GunType oldGun, GunSelectionSystem.GunType newGun)
        {
            Debug.Log("ACTIVATING GUN: " + newGun);
            selectedGun = newGun;
            ActivateGun(newGun);
        }
        
        public void ActivateGun(GunSelectionSystem.GunType gunType)
        {
            foreach (var gun in guns)
            {
                gun.SetActive(false);
            }

            guns[(int)gunType].SetActive(true);
            weapon = guns[(int)gunType].GetComponent<Weapon>(); // Ensure activeWeapon is updated
            
            if (weapon == null)
            {
                Debug.LogError("Active weapon is null! Make sure the Gun objects have a Weapon component.");
            }
        }

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = playerCameraPosition.position;
            Camera.main.gameObject.GetComponent<MouseLook>().playerBody = transform;
            
            floatingInfo.transform.localPosition = new Vector3(0, -0.3f, 0.6f);
            floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            string name = "Player" + Random.Range(100, 999);
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            CmdSetupPlayer(name, color);
            GameManager.Instance.localPlayer = gameObject;
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

            if (!isPlayerInGame) return;

            if (!isLocalPlayer)
            {
                // make non-local players run this
                floatingInfo.transform.LookAt(Camera.main.transform);
                return;
            }
            var moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

            _characterController.Move(moveDirection * (moveSpeed * Time.deltaTime));

            if (Time.time > weaponCooldownTime)
            {
                canShoot = true;
            }

            /*if (Input.GetAxis("Mouse ScrollWheel") > 0) //Fire2 is mouse 2nd click and left alt
            {
                selectedGun += 1;

                if ((int)selectedGun > guns.Length) 
                    selectedGun = 0; 

                //CmdChangeActiveWeapon((int)selectedGun);
            }*/

            //if (SceneManager.GetActiveScene("Lobby"))
            //{
            //isCurrentScene = !enabled;
                if (Input.GetButton("Fire1") && weapon && canShoot && isLocalPlayer/*&& ammoManager.isReloading == false*/)
                {
                    Debug.Log("Player is shooting");
                    weaponCooldownTime = Time.time + weapon.cooldown;
                    canShoot = false;
                    guns[(int)selectedGun].GetComponent<AmmoManager>().Shoot(); 

                    CmdShootRay(); // Call shooting method
                }
            //}
            
            if (weapon == null)
            {
                Debug.LogError("activeWeapon is null! Make sure ActivateGun() is called before shooting.");
                return;
            }

        }

        public void SetLocalLoadout(GunSelectionSystem.GunType gun, PerkSystem.PerkType perk)
        {
            OnSelectedGunChanged(selectedGun, gun);
            GetComponent<PerkSystem>().selectedPerk = perk;
            if (isLocalPlayer)
            {
                Debug.Log(selectedGun.ToString());
                guns[(int)selectedGun].GetComponent<AmmoManager>().EquipWeapon();
            }
            SetPlayerLoadout(gun,perk);
        }
        // Method to set the player's loadout
        [Command]
        public void SetPlayerLoadout(GunSelectionSystem.GunType gun, PerkSystem.PerkType perk)
        {
            Debug.Log($"SetPlayerLoadout() - Received: Gun = {gun}, Perk = {perk}");

            selectedGun = gun;
            GetComponent<PerkSystem>().selectedPerk = perk;
        
            Debug.Log($"GameManager After Set - Gun = {selectedGun}, Perk = {perk}");
        }
        
        [Command]
        void CmdShootRay()
        {
            if (weapon == null) return; // Ensure weapon exists

            RaycastHit hit;
            Transform camTransform = Camera.main.transform; // Use player's camera

            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, weapon.range))
            {
                Debug.Log($"Hit: {hit.collider.name}");

                if (hit.collider.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
                {
                    playerHealth.TakeDamage(weapon.damage);
                }
            }

            RpcFireWeapon(); // Play effects for all players
        }



        [ClientRpc]
        void RpcFireWeapon()
        {
            GameObject bullet = Instantiate(weapon.bullet, weapon.firePosition.transform.position, weapon.firePosition.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * weapon.bulletSpeed;
            Destroy(bullet, 5f);
        }
        
        private SceneScript sceneScript;

        void Awake()
        {
            //allow all players to run this
            sceneScript = GameObject.FindObjectOfType<SceneScript>();
            //allows all players to run this
            sceneScript = GameObject.Find("GameManager").GetComponent<SceneReference>().sceneScript;
        }
        
        [Command]
        public void CmdSendPlayerMessage()
        {
            if (sceneScript) 
                uiStuff.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
        }
        
        void OnWeaponChanged(int _Old, int _New)
        {
            ActivateGun((GunSelectionSystem.GunType)_New);
        }

        [Command]
        public void CmdChangeActiveWeapon(int newIndex)
        {
            selectedGun = (GunSelectionSystem.GunType)newIndex;
        }

        public void EnterGame()
        {
            isPlayerInGame = true;
        }
    }
}
