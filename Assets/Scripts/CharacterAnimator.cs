using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator; // referenceto animator component
    NavMeshAgent agent; // we'll use this to get the current speed of the character
    const float locotionAnimationSmoothTime = .1f;

    void Start()
    {
        // intialize our types
        agent = GetComponent<NavMeshAgent>(); 
        animator = GetComponentInChildren<Animator>(); // GetComponentInChildren, because our animator component insnide Player object
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locotionAnimationSmoothTime, Time.deltaTime);
    }
}
