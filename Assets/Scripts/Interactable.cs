using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    bool hasInteracted = false;
    Transform player;


    public virtual void Interact() 
    {
        // method is meant to be overwritten by derived classes
        Debug.Log("Interacting with " + transform.name);
    }

    public void OnFocused(Transform playerTransform) 
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;

    }

    public void onDeFocused() 
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void Update() 
    {
        if (isFocus && !hasInteracted) 
        {
            // get distance between player and current position 
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            // check if player is inside the radius
            if (distance <= radius) 
            {
                Interact();
                hasInteracted = true;
            }
            
        }
    }
    void OnDrawGizmosSelected() 
    {
        // automatically set transform to itself, if one is not set.
        if (interactionTransform == null) 
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
        
    }
}
