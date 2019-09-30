using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireFighterController))]
public class FireFighter : DelegationActor
{
    public FireFighterController controller;
    private SetHead headSetter;
    private string [] namesList = {"FireFighter", "Abel", "Bower", "Chen", "Davis", "Estrada", "Feldman", "Hewett", "Lewis", "Miller", "Nassar", "Sulivan", "Turner"};
    public string name = "FireFighter";
    public ToonLines outline;

    public override void Init() 
    {
        controller = GetComponentInChildren<FireFighterController>();   
        headSetter = GetComponent<SetHead>(); 
        name = namesList[headSetter.headIndex];

        outline =  GetComponent<ToonLines>();

        DelegationManager.Instance.menu.AddCharacter(this);
    }

    public void SetOutline(bool active)
    {
        if (active)
        {
            outline.EnableLine();
        }
        else
        {
            outline.DisableLine();
        }
    }

    public int GetCurrentHead()
    {
        return headSetter.headIndex;
    }
}
