using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireFighterController))]
public class FireFighter : DelegationActor
{
    public FireFighterController controller;
    public float timeToSearch = 35;
    public bool inDoorway = false;
    private SetHead headSetter;
    private string [] namesList = {"FireFighter", "Abel", "Bower", "Chen", "Davis", "Estrada", "Feldman", "Hewett", "Lewis", "Miller", "Nassar", "Sulivan", "Turner"};
    public string name = "FireFighter";
    public ToonLines outline;

    public CharacterButton charaButton;

    public override void Init() 
    {
        controller = GetComponentInChildren<FireFighterController>();   
        headSetter = GetComponent<SetHead>(); 
        name = namesList[headSetter.headIndex];

        outline =  GetComponent<ToonLines>();

        charaButton = DelegationManager.Instance.menu.AddCharacter(this);
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

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Door")
            inDoorway = true;
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Door")
            inDoorway = true;
    }
}
