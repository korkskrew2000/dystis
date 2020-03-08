using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    EquipmentManager equipmentMg;
    public Transform bodyPanel;
    InventorySlot[] slots;
    EquipmentHolder[] holders;

    // Start is called before the first frame update
    void Start()
    {
        equipmentMg = EquipmentManager.instance;
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        equipmentMg.onEquipmentChanged += UpdateEquipmentUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        holders = bodyPanel.GetComponentsInChildren<EquipmentHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI() {
        print("Updating Inventory UI");

        for (int i = 0; i < slots.Length; i++) {
            if(i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
    }

   void UpdateEquipmentUI(Equipment newItem, Equipment oldItem)
    {
        print("Updating Equipment UI");
        int slotIndex;
        if (newItem != null)
        {
            slotIndex = (int)newItem.equipSlot;
            holders[slotIndex].AddEquipment(newItem);
        }
        else
        {
            slotIndex = (int)oldItem.equipSlot;
            holders[slotIndex].RemoveEquipment();
        }
    }
}
