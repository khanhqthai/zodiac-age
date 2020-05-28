using System;
using UnityEngine;

public class ItemPickUp : Interactable
{

    public Item item;

    public override void Interact()
    {
        // execute base class function
        // update later to  do cool stuff
        base.Interact();
        PickUp();

    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.name);


        //Add item to inventory
        bool wasPickedUp = Inventory.instance.Add(item);

        // if item was picked up successfully
        // remove item from world, when picked up
        if (wasPickedUp) {
            Destroy(gameObject);
        }

    }
}
