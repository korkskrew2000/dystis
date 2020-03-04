using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button itemButton;
    public Button removeButton;

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        itemButton.interactable = true;
        removeButton.interactable = true;
    }

    public void RemoveItem() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        itemButton.interactable =  false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton() {
        Inventory.instance.Remove(item);
    }

    public void UseItem() {
        if(item != null) {
            item.Use();
        }
    }
}
