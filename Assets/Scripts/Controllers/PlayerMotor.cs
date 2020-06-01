using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// make sure we always have a NevMeshAgent to get
// we'll require a NavMeshAgent with an attribute
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update() 
    {
        if (target != null) 
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }



    public void MoveToPoint(Vector3 point) 
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 1f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        
        // 0f on y to keep the player from looking up an down.
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        
        // Quaternion.Slerp to smooth the transition
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *  5f);
    }
}
