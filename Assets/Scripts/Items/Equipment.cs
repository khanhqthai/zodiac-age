using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot; // slot to store equipment in
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions; 

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


public enum EquipmentSlot { Head, Chest, Hands, Weapon, Shield, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Chest } // corresponds to body blendshapes(BaseBody)