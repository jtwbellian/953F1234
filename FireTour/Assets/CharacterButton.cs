using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Status {none = -1, stairs, hydrant, door, running, ladder, hose};
public class CharacterButton : VRButton
{
    public Image avatar;
    public Image statusIcon = null;
    public TextMeshProUGUI _name;
    //public TextMeshProUGUI _location;
    //public TextMeshProUGUI _action;
    public TextMeshProUGUI _status;

    public UICircleFill progressMeter;
    public DelegationMenu delegationMenu;
    public FireFighter actor;
    public List<Sprite> icons;

    private Color c_white = new Color32(255, 255, 255, 100);
    private Color c_busy = new Color32(255, 200, 100, 50);
    private Color c_hidden = new Color32(255, 255, 255, 0);

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

    public void SetStatus(Status status, string description)
    {
        var numStates = System.Enum.GetNames(typeof(Status)).Length;

        if (icons.Count <  numStates - 1) // Don't include the "none" -1 state 
        {
            Debug.Log("Error, icon images were not set in inspector for " + (numStates - icons.Count).ToString()  + " state icons. ");
            return;
        }

        if (status == Status.none)
        {
            statusIcon.color = c_hidden;
            avatar.color = c_white;
            return;
        }

        statusIcon.sprite = icons[(int)status];
        statusIcon.color = c_white;
        avatar.color = c_busy;

    }

    public void SelectCharacter()
    {
        delegationMenu.ClearSelection();
        delegationMenu.SetActionPanel(true);
        delegationMenu.SetTab(0);
        delegationMenu.currentCharacter.text = actor.name;

        // Update label to "awaiting orders"
        if (_status.text == "Standing By")
        {
            _status.text = "Awaiting Orders";
        }

        delegationMenu.SetCurrentAction(_status.text);

        DelegationManager.Instance.Selection(actor.gameObject);
        actor.SetOutline(true);
        progressMeter.SetRing(true);
    }
}
