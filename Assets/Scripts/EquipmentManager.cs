using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of equipment. Has functions for removing and adding items*/
public class EquipmentManager : MonoBehaviour
{
    //singleton pattern
    public static EquipmentManager instance;
   
    private void Awake() 
    {
        instance = this;
    }

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;

    private Equipment[] currentEquipment; // Items we currently have equiped
    private SkinnedMeshRenderer[] currentMeshes; 
    

    // delegate to notify subscribers of (event)changes in Equipment
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;
    private void Start()
    {
        inventory = Inventory.instance;

        // intiliazed our equipment array with size from Equipment slot enum
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        currentMeshes = new SkinnedMeshRenderer[numSlots];
        EquipDefaultItems();
    }

    public void Equip(Equipment newItem) 
    {
        // Find out what slot the item fits in
        // We can get index value from Enum(ie. Head =0 , Chest = 1...)
        int slotIndex = (int) newItem.equipSlot;
       
        Equipment oldItem = Unequip(slotIndex);


        // An item has been equiped, so we notify subcribers of change using callback
        if (onEquipmentChanged != null) 
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        
        SetEquipmentBlendShapes(newItem,90);

        currentEquipment[slotIndex] = newItem; // insert item into slot
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex) 
    {
        // check if there is an item equiped at the slot index
        // if so, then remove, and put back into the inventory
        if (currentEquipment[slotIndex] != null) 
        {
            
            if (currentMeshes[slotIndex] != null) 
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem); // item back to inventory
            currentEquipment[slotIndex] = null; // remove item from slot by setting to null

            // notify subcribers of changes
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;    
        }
        return null;
    }

    public void UnequipAll() 
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
            
        }
        EquipDefaultItems(); // equip default item, when player unequip all current items

    }

    private void Update() 
    {
        // pressing "U" on keyboard will unequip all items
        if (Input.GetKeyDown(KeyCode.U)) 
        {
            UnequipAll();
        }
    }

    // shrinks body mesh, so equipment can fit better.. don't really it, since our character is already small
    // and modeled correctly. But here just incase
    private void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    //
    public void EquipDefaultItems() 
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }


}
