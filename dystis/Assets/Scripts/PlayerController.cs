using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Camera cam;

    public Interactable focus;

    void Start() {
        cam = Camera.main;
    }

    void Update() {

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        // If we press left mouse button
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

                //if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                if (Physics.Raycast(ray, out hit, 100)) {

                    Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                RemoveFocus();
            }
        }

        // If we press right mouse button
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null) {
                focus.OnDefocused();
            }

            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus() {
        if (focus != null) {
            focus.OnDefocused();
        }

        focus = null;
    }

}

