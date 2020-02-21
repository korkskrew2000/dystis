using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static Inventory instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    public void Add(Item item) {
        if (!item.isDefaultItem) {
            items.Add(item);
        }
    }

    public void Remove(Item item) {
        items.Remove(item);
    }
}
