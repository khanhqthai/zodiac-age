using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item item; // variable to keep track of current item in the slot

    public Image icon; // reference to the icon image in the UI provided by UnityEngine.UI;
    public Button removeButton; // reference to remove button

    // add item to the slot method
    public void AddItem(Item newItem) 
    {
        if (item != newItem) 
        {
            item = newItem;

            icon.sprite = item.icon;  // InventorySlot's sprite to the Item's icon image being picked up
            icon.enabled = true; // enable to show icon in the inventory slot
            removeButton.interactable = true;
        }
        
    }

    public void ClearSlot() 
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }


    public void OnRemoveButton() 
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem() 
    {
        if (item !=null) 
        {
            item.Use();
        }
    }
}
