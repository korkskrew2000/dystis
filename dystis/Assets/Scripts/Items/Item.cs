using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "Item Name";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isQuestItem = false;
    public GameObject model;

    public virtual void Use() {
        Debug.Log("Using " + name);
    }

    //bool argumentti kertoo, pudotetaanko item fyysisesti pelikentälle (esim.
    //equipmenttia puettaessa item poistetaan inventorysta, mutta EI pudoteta)
    public void RemoveFromInventory(bool shouldBeDropped) {
        Inventory.instance.Remove(this, shouldBeDropped);
    }
}
