using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireFighterController))]
public class FireFighter : DelegationActor
{
    public FireFighterController controller;

    private void Start() 
    {
        controller = GetComponentInChildren<FireFighterController>();   
    }
}
