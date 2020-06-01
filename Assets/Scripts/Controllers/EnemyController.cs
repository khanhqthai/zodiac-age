using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float agroRadius = 10f;

    private Transform target;
    private NavMeshAgent agent;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= agroRadius) 
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance) 
            {
                //attack target
                FaceTarget();
            }
        }
    }

    private void FaceTarget() 
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // slerp to smooth out the turn
        
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRadius);

    }
}
