using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Handles interaction with enemies*/

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private PlayerManager playerManager;
    private CharacterStats myStats;

    private void Start() 
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.GetComponent<CharacterCombat>();
        if (playerCombat != null) 
        {
            playerCombat.Attack(myStats);
        }
    }
}
