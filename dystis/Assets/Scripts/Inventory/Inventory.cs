using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Camera cam;
    public List<Item> items = new List<Item>();
    public int inventorySpace = 20;
    public GameObject itemPrefab;
    public GameObject questItemPanel;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public static Inventory instance;

    private void Awake() {
        cam = Camera.main;

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

            if (onItemChangedCallBack != null) onItemChangedCallBack.Invoke();
        }
        return true;
    }

    //toinen argumentti kertoo, pudotetaanko item fyysisesti pelikentälle (esim.
    //equipmenttia puettaessa item poistetaan inventorysta, mutta EI pudoteta)
    public void Remove(Item item, bool shouldBeDropped) {
        if (!item.isQuestItem)
        {
            //instantioidaan pudotettava item pelaajan eteen
            if (shouldBeDropped)
            {
                Vector3 playerTransform = cam.transform.position + cam.transform.forward;
                GameObject prefabToInstantiate = itemPrefab;
                if (item.model != null)
                {
                    prefabToInstantiate = item.model;
                }
                GameObject droppedItem = Instantiate(prefabToInstantiate, playerTransform, Quaternion.identity);
                ItemPickUp droppedItemPickUp = droppedItem.GetComponent<ItemPickUp>();
                if (droppedItemPickUp != null)
                {
                    droppedItemPickUp.item = item;
                }
            }
            //poistetaan item inventorysta
            items.Remove(item);
            if (onItemChangedCallBack != null) onItemChangedCallBack.Invoke();
        }
        else
        {
            questItemPanel.SetActive(true);
        }
    }
}
