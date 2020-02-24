using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int inventorySpace = 20;

    public static Inventory instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    public bool Add(Item item) {
        if (!item.isDefaultItem) {
            if (items.Count >= inventorySpace) {
                print("Inventory is full.");
                return false;
            }
            items.Add(item);
        }
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
    }
}
