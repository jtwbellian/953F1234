using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(FireFighterController))]
public class FireFighter : DelegationActor
{
    public FireFighterController controller;
    public float timeToSearch = 35;
    public Door doorway = null;
    private SetHead headSetter;
    private string [] namesList = {"FireFighter", "Abel", "Bower", "Chen", "Davis", "Estrada", "Feldman", "Hewett", "Lewis", "Miller", "Nassar", "Sulivan", "Turner"};
    public string name = "FireFighter";
    public ToonLines outline;
    public TextMeshProUGUI namePlate;

    public CharacterButton charaButton;

    public override void Init() 
    {
        controller = GetComponentInChildren<FireFighterController>();   
        headSetter = GetComponent<SetHead>(); 
        name = namesList[headSetter.headIndex];

        outline =  GetComponent<ToonLines>();

        charaButton = DelegationManager.Instance.menu.AddCharacter(this);
        namePlate = GetComponentInChildren<TextMeshProUGUI>();
        namePlate.text = name.ToUpper();
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
            {
                doorway = other.GetComponentInChildren<Door>(); 
                doorway.Open();
            }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Door")
            doorway = null; 
    }
}
