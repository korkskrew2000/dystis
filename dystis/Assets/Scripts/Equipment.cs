﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public override void Use() {
        base.Use();
        EquipmentManager.instance.Equip(this);
        bool shouldBeDropped = false;
        RemoveFromInventory(shouldBeDropped);
    }
}

public enum EquipmentSlot { Head, Torso, Legs, RightHand, LeftHand, Feet }