using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    void Start()
    {
        // subscribe to onEquipmentChanged
        EquipmentManager.instance.onEquipmentChanged += OnEquipMentChanged;
    }

    
    // update armor stats on callback trigger 
    private void OnEquipMentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null) 
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null) 
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
        
    }

}
