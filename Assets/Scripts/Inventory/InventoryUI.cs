using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //Inventory inventory;
    public Transform itemsParents;
    public GameObject inventoryUI; // reference to InventoryUI, we'll use it to disable and enable the Inventory UI.

    InventorySlot[] slots;

    // Start is called before the first frame update
    private void Start()
    {
        // this is weird, the whole idea of a singleton is so you have only one instance
        // and can call it anywhere
        // why have a reference to it, to make code cleaner?
        // guys says it help caches it.
        
        Inventory.instance.onItemChangedCallBack += UpdateUI;
        slots = itemsParents.GetComponentsInChildren<InventorySlot>();
    }

    // We will not be using Update() method to update the inventoryUI because, all our items are static, so we don't need to constantly update
    private void Update()
    {
        if (Input.GetButtonDown("Inventory")) 
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    private void UpdateUI() 
    {
        Debug.Log("UPDATING UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.instance.items.Count)
            {
                slots[i].AddItem(Inventory.instance.items[i]);
            }
            else 
            {
                slots[i].ClearSlot();
            }
        }
    }
}
