using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    //Singleton pattern
    public static Inventory instance;
    void Awake() 
    {
        if (instance != null) 
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }
    


    public List<Item> items = new List<Item>();
    public int inventorySpace = 10;

    // delegate to notify subscribers of (event)changes in the inventory
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public bool Add(Item item) 
    {
        // only add item, if is not a default item, and if inventory has enough space
        if (!item.isDefaultItem) 
        {
            if (items.Count >= inventorySpace) 
            {
                Debug.Log("Inventory full!");
                return false; 
            }
            items.Add(item);

            // check if not null before invoke subscribed functions 
            if (onItemChangedCallBack != null) 
            {
                onItemChangedCallBack.Invoke();
            }
            
        }
        return true;
    }

    public void Remove(Item item) 
    {
        items.Remove(item);

        // check if not null before invoke subscribed functions 
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
}
