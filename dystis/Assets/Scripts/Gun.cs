using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Inventory/Gun")]
public class Gun : Equipment
{
    public int fireRate;
    public int holderIndex;

    void Reset()
    {
        equipSlot = EquipmentSlot.RightHand;
    }

    public override void Use()
    {
        //base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
