using UnityEngine.EventSystems;
using UnityEngine;

public class JoniPlayerController : MonoBehaviour
{

    Camera cam;
    EquipmentManager equipmentMg;

    int interactablesMask = 1 << 9; // To check if we are looking at interactable thing.
    int ignoreRayMask = ~(1 << 2);
    bool interactableNotification = false; // Notify user about interactable thing.
    bool lookingAtInteractable = false;
    bool canShoot = true;
    int weaponDamage;

    public float maxDistance = 2; // Max interaction distance.
    public Interactable focus;
    public GameManager gameManager;
    public GameObject interactableInfo;
    public GameObject inventoryPanel;

    // == Teleport ================
    public Transform tpDestination;
    TeleportActivation tpActivation;
    float teleportFadespeed = 0.5f;
    public bool teleportStarting = false;
    public bool teleportOnGoing = false;
    public bool teleportEnding = false;
    bool teleportAudioPlaying = false;
    CanvasGroup fadeOverlay;
    // ============================

    void Start()
    {
        equipmentMg = EquipmentManager.instance;
        equipmentMg.onEquipmentChanged += UpdateWeapon;
        cam = Camera.main;
        // For fading... CanvasGroup is in the PlayerFadeCanvas
        fadeOverlay = GameManager.FindObjectOfType<CanvasGroup>();
    }

    void UpdateWeapon(Equipment newitem, Equipment olditem)
    {

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

    void SwitchState(GameObject gO)
    {
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
        if (Input.GetButtonDown("Inventory"))
        {
            SwitchState(inventoryPanel);
        }
        //if (EventSystem.current.IsPointerOverGameObject()) {
        //    return;
        //}

        var rayz = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitz;
        if (Physics.Raycast(rayz, out hitz, maxDistance, interactablesMask))
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

        // If we press left mouse button
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 100, movementMask)) {
            if (Physics.Raycast(ray, out hit, 1000, ignoreRayMask))
            {

                Debug.Log("We hit: " + hit.collider.name + " " + hit.point);

                NPCControllerV2 npc = hit.transform.GetComponent<NPCControllerV2>();
                if(npc != null)
                {
                    npc.DamageIt(20);
                }
                //RemoveFocus();
            }
        }

        // If we press right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
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
            fadeOverlay.alpha += Time.deltaTime * teleportFadespeed;
            Debug.Log("Faderoverlay Alpha: " + fadeOverlay.alpha);
            if (fadeOverlay.alpha >= 1f) // when the screen is fully Black
            {
                teleportStarting = false;
                teleportOnGoing = true;
                teleportEnding = false;
                teleportAudioPlaying = false;
                //Debug.Log("Faderoverlay Alpha: " + fadeOverlay.alpha);
            }
        }

        if (teleportOnGoing)
        {
            Debug.Log("Teleport destination:" + tpDestination.name);
            transform.position = tpDestination.transform.position + new Vector3(0, 0.2f, 0);
            teleportStarting = false;
            teleportOnGoing = false;
            teleportEnding = true;
            AudioFW.Play("doorclose");
        }

        if (teleportEnding)
        {
            if (!teleportAudioPlaying)
            {
                AudioFW.Play("doorclose");
                teleportAudioPlaying = true;
            }
            fadeOverlay.alpha -= Time.deltaTime * teleportFadespeed;
            if (fadeOverlay.alpha <= 0f)
            {
                teleportStarting = false;
                teleportOnGoing = false;
                teleportEnding = false;
                teleportAudioPlaying = false;
                EnablePlayerMovement(false);
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


}

