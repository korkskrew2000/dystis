using UnityEngine;
using UnityEngine.UI;

public class EquipmentHolder : MonoBehaviour
{
    Equipment equipment;
    public Image icon;
    public Button unquipmentButton;
    public Button removeButton;

    public void AddEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        if (equipment.name == "Shotgun" || equipment.name == "Pistol")
        {
            Debug.Log(equipment.name);
            AudioFW.Play("shotgunequip");
        }
        icon.sprite = equipment.icon;
        icon.enabled = true;
        unquipmentButton.interactable = true;
        removeButton.interactable = true;
    }

    public void RemoveEquipment()
    {
        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
        unquipmentButton.interactable = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        if (equipment != null)
        {
            int slotIndex = (int)equipment.equipSlot;
            EquipmentManager.instance.Remove(slotIndex);
        }
    }

    public void UnequipItem()
    {
        if (equipment != null)
        {
            int slotIndex = (int)equipment.equipSlot;
            EquipmentManager.instance.Unequip(slotIndex);
        }
    }
}
