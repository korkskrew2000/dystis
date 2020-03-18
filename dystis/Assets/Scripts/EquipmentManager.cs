using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    private void Awake() {
        instance = this;
    }

    #endregion

    public Equipment[] currentEquipment;
    public GameObject itemPrefab;

    public delegate void OnEquipmentChanged(Equipment newitem, Equipment olditem);
    public OnEquipmentChanged onEquipmentChanged;
    Inventory inventory;
    Camera cam;

    void Start() {
        cam = Camera.main;
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null) {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
    }

    public void Unequip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }

    }

    public void UnequipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            Unequip(i);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }

    public void Remove(int slotIndex)
    {
        Vector3 playerTransform = cam.transform.position + cam.transform.forward;
        GameObject droppedItem = Instantiate(itemPrefab, playerTransform, Quaternion.identity);
        ItemPickUp droppedItemPickUp = droppedItem.GetComponent<ItemPickUp>();
        
        if (currentEquipment[slotIndex] != null)
        {
            if (droppedItemPickUp != null)
            {
                droppedItemPickUp.item = currentEquipment[slotIndex];
            }
            if (onEquipmentChanged != null) onEquipmentChanged.Invoke(null, currentEquipment[slotIndex]);
        }
        currentEquipment[slotIndex] = null;
    }
}
