using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerContoller : MonoBehaviour

{
    // reference to our camera
    Camera cam;
    PlayerMotor motor;

    public LayerMask movementMask;
    public Interactable focus;
    public 


    // Start is called before the first frame update
    void Start()
    {
        // set cam to the main camera
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // if our point is over a game object(ie our inventory or other UI objects), do nothing
        if (EventSystem.current.IsPointerOverGameObject()) 
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) 
            {
                // move to our player to what we hit
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);

                // stop focusing on any object
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // check if we hit an interactable object
                // if we did, set as our focus
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable !=null) 
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        // check if we already had a previous focus
        // if not then update
        if (newFocus != focus ) 
        {
            if (focus !=null ) 
            {
                focus.onDeFocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        
        newFocus.OnFocused(transform);
        
    }

    private void RemoveFocus()
    {
        // null check   
        if (focus !=null) 
        {
            focus.onDeFocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }
}
