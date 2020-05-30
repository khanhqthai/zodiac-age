using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();

        // equip the item
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}


public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }