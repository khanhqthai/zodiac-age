using UnityEngine;


// adds the to create asset menu in unity(right click and select)
[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    // overrides name property default value from ScriptableObject
    new public string name = "New Item";
    
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use() 
    {
        //Use item code will be defined by derived classes
        //Since Item use can have different effects
        // ie. potions item heal, town portal item teleport
        Debug.Log("Using " + name);
    }
}
