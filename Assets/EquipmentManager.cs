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

    private void Start()
    {
        
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem) 
    { 
        
    }
}
