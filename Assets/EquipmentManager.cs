using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //singleton pattern
    public static EquipmentManager instance;
   
    private void Awake() 
    {
        instance = this;
    }

    Equipment[] currentEquipment;

    // delegate to notify subscribers of (event)changes in Equipment
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;
    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots]; // intiliazed our equipment array with size from Equipment slot enum
    }

    public void Equip(Equipment newItem) 
    {
        // We can get index value from Enum(ie. Head =0 , Chest = 1...)
        int slotIndex = (int) newItem.equipSlot;

        Equipment oldItem = null;

        // before we equip the new item
        // we want to check if the player already has an item equiped in slot
        // if so, we will move that item back to  the inventory before equiping the new item.
        if (currentEquipment[slotIndex]!=null) 
        {
           
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        // notify subcribers of changes
        if (onEquipmentChanged != null) 
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
 
    }

    public void Unequip(int slotIndex) 
    {
        // check if there is an item equiped at the slot index
        // if so, then remove, and put back into the inventory
        if (currentEquipment[slotIndex] != null) 
        {
            Equipment oldItem = currentEquipment[slotIndex];
            
            inventory.Add(oldItem); // item back to inventory
            currentEquipment[slotIndex] = null; // remove item from slot by setting to null

            // notify subcribers of changes
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll() 
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    public void Update() 
    {
        // pressing "U" on keyboard will unequip all items
        if (Input.GetKeyDown(KeyCode.U)) 
        {
            UnequipAll();
        }
    }

}
