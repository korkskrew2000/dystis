using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    EquipmentManager equipmentMg;
    //Inventory inventory;
    Gun currentGun;
    Equipment rightHand;

    public Quest quest;
    public int health = 100;
    public int experience = 0;
    public float money = 0.95f;

    int interactablesMask = 1 << 9; // To check if we are looking at interactable thing.
    int ignoreRayMask = ~(1 << 2);
    bool interactableNotification = false; // Notify user about interactable thing.
    bool lookingAtInteractable = false;
    bool canShoot = true;
    int weaponDamage;
    int weaponFireRate;
    int weaponIndex = -1;
    ParticleSystem weaponMuzzle;
    Transform[] weapons; // lista kamerassa näkyivstä aseista WeaponHolderin alla

    public float interactionDistance = 2; // Max interaction distance.
    public Interactable focus;
    public GameObject interactableInfo;
    public GameObject impactEffectConcrete;
    public GameObject impactEffectBlood;
    public GameObject weaponHolder;
    public GameObject menuPanel;
    public GameObject inventoryPanel;
    public GameObject questPanel;
    public GameObject placeholderPanel;
    public GameObject settingsPanel;

    // == Teleport ================
    public Transform tpDestination;
    TeleportActivation tpActivation;
    float teleportFadespeed = 0.5f;
    public bool teleportStarting = false;
    public bool teleportOnGoing = false;
    public bool teleportEnding = false;
    bool teleportAudioPlaying = false;
    //CanvasGroup fadeOverlay;
    public GameObject fadeOverlay;
    CanvasGroup fadeOverlayCG;

    // ============================

    void Start()
    {
        equipmentMg = EquipmentManager.instance;
        equipmentMg.onEquipmentChanged += UpdateWeapon;
        //inventory = Inventory.instance;


        cam = Camera.main;

        // For fading... CanvasGroup is in the PlayerFadeCanvas
        fadeOverlayCG = fadeOverlay.GetComponent<CanvasGroup>();

        // alustetaan kameraan näkyvien aseiden lista WeaponHolderin lapsiobjekteilla
        weapons = new Transform[weaponHolder.transform.childCount];
        for (int i = 0; i < weaponHolder.transform.childCount; i++)
        {
            weapons[i] = weaponHolder.transform.GetChild(i);
        }

        Debug.Log("weapons length " + weapons.Length);
    }

    void UpdateWeapon(Equipment newItem, Equipment oldItem)
    {
        rightHand = newItem;

        // päivitetään tieto pelaajan tämänhetkisestä aseesta
        if(rightHand is Gun)
        {
            currentGun = (Gun)rightHand;
            weaponDamage = currentGun.damageModifier;
            weaponFireRate = currentGun.fireRate;
            weaponIndex = currentGun.holderIndex;
        } else
        {
            currentGun = null;
            weaponIndex = -1;
        }

        // asetetaan oikea ase näkymään kamerassa
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weaponIndex == i)
            {
                GameObject weapon = weaponHolder.transform.GetChild(i).gameObject;
                weapon.SetActive(true);
                weaponMuzzle = weapon.transform.GetComponentInChildren<ParticleSystem>();
            }
            else
            {
                weaponHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void DisablePlayerMovement(bool visibleCursor)
    {
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = false;
        GetComponent<FirstPersonAIO>().playerCanMove = false;
        GetComponent<FirstPersonAIO>().enableCameraMovement = false;
        GetComponent<FirstPersonAIO>().autoCrosshair = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = visibleCursor;
        canShoot = false;
    }

    public void EnablePlayerMovement(bool visibleCursor)
    {
        GetComponent<FirstPersonAIO>().playerCanMove = true;
        GetComponent<FirstPersonAIO>().enableCameraMovement = true;
        GetComponent<FirstPersonAIO>().autoCrosshair = true;
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = visibleCursor;
        canShoot = true;
    }

   public void SwitchStateAndMovement(GameObject gO)
    {
        Debug.Log("Switch State Occured!)");
        if (gO.activeSelf == true)
        {
            gO.SetActive(false);
            EnablePlayerMovement(false);
        }
        else
        {
            gO.SetActive(true);
            DisablePlayerMovement(true);
        }
    }

    void Update()
    {
        // Button "i" opens menu + inventory.
        if (Input.GetKeyDown(KeyCode.I) && !menuPanel.activeSelf) {
            Debug.Log("Menu Button Pressed");
            SwitchStateAndMovement(menuPanel);
            inventoryPanel.SetActive(true);
        // If menu is already open close it and all panels.
        } else if(Input.GetKeyDown(KeyCode.I) && menuPanel.activeSelf) {
            inventoryPanel.SetActive(false);
            questPanel.SetActive(false);
            placeholderPanel.SetActive(false);
            settingsPanel.SetActive(false);
            SwitchStateAndMovement(menuPanel);
        }

        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    SwitchStateAndMovement(inventoryPanel);
        //}

        //if (EventSystem.current.IsPointerOverGameObject()) {
        //    return;
        //}

        var rayz = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitz;
        if (Physics.Raycast(rayz, out hitz, interactionDistance, interactablesMask))
        {
            lookingAtInteractable = true;
            if (!interactableNotification && lookingAtInteractable)
            {
                interactableNotification = true;
                Debug.Log("You can do something here...");
                interactableInfo.SetActive(true);
                Debug.DrawLine(rayz.origin, hitz.point, Color.red, 0.5f);
            }

        }
        else
        {
            interactableInfo.SetActive(false);
            lookingAtInteractable = false;
            interactableNotification = false;
        }

        // If we press left mouse button and we have gun and we can shoot
        if (Input.GetMouseButtonDown(0) && currentGun != null && canShoot)
        {
            Debug.Log("We just shot with a gun!");
            //if (currentGun == "shotgun") {
                AudioFW.Play("shotgunshot");
            //}
            weaponMuzzle.Play();
            Debug.Log("Muzzle: " + weaponMuzzle);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 100, movementMask)) {
            if (Physics.Raycast(ray, out hit, 1000, ignoreRayMask))
            {

                Debug.Log("We hit: " + hit.collider.name + " " + hit.point);

                NPCControllerV2 npc = hit.transform.GetComponent<NPCControllerV2>();
                if (npc != null)
                {
                    npc.DamageIt(weaponDamage);
                    GameObject holeNPC = Instantiate(impactEffectBlood, hit.point, Quaternion.LookRotation(hit.normal));
                    holeNPC.transform.parent = hit.transform;
                }
                else
                {
                    GameObject hole = Instantiate(impactEffectConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                    hole.transform.parent = hit.transform;
                }

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * weaponDamage * 3);
                }

                //GameObject hole = Instantiate(impactEffectConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                //hole.transform.parent = hit.transform;
                //RemoveFocus();
            }
        }

        // If we press right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    Debug.Log("We hit interactable: " + interactable.name + " " + hit.point);
                }
            }
        }

        // === Teleporting ==================================

        if (teleportStarting)
        {
            if (!teleportAudioPlaying)
            {
                AudioFW.Play("dooropen");
                teleportAudioPlaying = true;
            }

            DisablePlayerMovement(false);
            fadeOverlayCG.alpha += Time.deltaTime * teleportFadespeed;
            //Debug.Log("Faderoverlay Alpha: " + fadeOverlayCG.alpha);
            if (fadeOverlayCG.alpha >= 1f) // when the screen is fully Black
            {
                teleportStarting = false;
                teleportAudioPlaying = false;
                teleportOnGoing = true;
                //Debug.Log("Faderoverlay Alpha: " + fadeOverlay.alpha);
            }
        }

        if (teleportOnGoing)
        {
            focus = null;
            
            //Debug.Log("Teleport ongoing to destination:" + tpDestination.parent.name);
            //Debug.Log("Player angles before teleportOngoing: " + transform.eulerAngles);
            //Debug.Log("Camera angles before teleportOngoing: " + cam.transform.eulerAngles);

            // Rotate player to look exit direction (tpDestination.forward) when arriving at destination.
            // tpDestination = TeleporterExit object.

            transform.position = tpDestination.transform.position + new Vector3(0, 0.2f, 0);

            //Debug.Log("Player angles after teleportOngoing: " + transform.eulerAngles);
            //Debug.Log("Camera angles after teleportOngoing: " + cam.transform.eulerAngles);
  
            teleportOnGoing = false;
            teleportEnding = true;
        }

        if (teleportEnding)
        {
            if (!teleportAudioPlaying)
            {
                AudioFW.Play("doorclose");
                teleportAudioPlaying = true;
            }
            fadeOverlayCG.alpha -= Time.deltaTime * teleportFadespeed;
            if (fadeOverlayCG.alpha <= 0f)
            {
                teleportEnding = false;
                teleportAudioPlaying = false;
                EnablePlayerMovement(false);
                //Debug.Log("Player angles after teleportEnding: " + transform.eulerAngles);
                //Debug.Log("Camera angles after teleportEnding: " + cam.transform.eulerAngles);
                
            }
        }

        // ================================================

    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
    }

    
    
    // Quest related below...

    // Very basic item...
    public void QuestTransferItem() {
        experience += 1;
    }

    public void QuestDeliverMysteriousLetter() {
        experience += 10;
        money += 50;
    }

    public void QuestDeliverPizza() {
        experience += 5;
        money += 5;
    }

    public void QuestKillNPCHuman() {
        experience += 10;
        health -= 10;
    }

    public void QuestKillNPCSnake() {
        experience += 1;
        health -= 1;
    }

}