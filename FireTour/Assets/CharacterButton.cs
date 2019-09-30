using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterButton : VRButton
{
    public Image avatar;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _location;
    public TextMeshProUGUI _action;
    public UICircleFill progressMeter;

    public DelegationMenu delegationMenu;

    public FireFighter actor;


    // Start is called before the first frame update
    void Start()
    {
        delegationMenu = GetComponentInParent<DelegationMenu>();
    }

    public void SetSource(FireFighter source)
    {
        name = source.name + "Button";
        actor = source;
        _name.text = source.name;
    }

    public void SetImage(Sprite sprite)
    {
        avatar.sprite = sprite; 
    }

    public void SelectCharacter()
    {
        delegationMenu.ClearSelection();
        delegationMenu.SetActionPanel(true);
        delegationMenu.SetTab(0);
        delegationMenu.currentCharacter.text = actor.name;
        delegationMenu.currentCharacterState.text = "";
        DelegationManager.Instance.Selection(actor.gameObject);
        actor.SetOutline(true);
        progressMeter.SetRing(true);
    }
}
