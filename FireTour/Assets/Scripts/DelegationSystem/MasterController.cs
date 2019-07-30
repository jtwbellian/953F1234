using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    // only one MasterController at a time.
    private static MasterController _instance;
    public static MasterController Instance { get { return _instance; } }
    // chainState is the current state that the player is in since there is a
    // chain of events.  First state is selecting the character, then location,
    // then action.
    public int chainState;
    public GameObject targetSpace;
    public Actor[] actors;
    public Actor newAssignee;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        chainState = 1;   
        actors = GameObject.FindObjectsOfType<Actor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetSpace != null){
            switch(chainState){
            // select character
            case 1: switch(targetSpace.tag){
                    case "Untagged": 
                            Debug.Log("Character select menu!");
                            // newAssignee = selectedCharacterMenu();
                            // check if there is an actual selection first.
                            chainState = 2;
                            targetSpace = null;
                            break;
                    case "location": 
                            Debug.Log("Auto assign character and select location.");
                            EventManager.autoAssign();
                            Debug.Log(newAssignee.actorName);
                            newAssignee.assignedLocation = targetSpace;
                            chainState = 3; // select action state
                            targetSpace = null;
                            break;
                    case "action": 
                            Debug.Log("Auto assign idle character and then move perform action.");
                            EventManager.autoAssign();
                            Debug.Log(newAssignee.actorName);
                            newAssignee.assignedAction = targetSpace;
                            chainState = 2; // select location state
                            targetSpace = null;
                            break;
                    default: 
                            Debug.Log("Character select menu does not appear since a character was directly targeted.");
                            newAssignee = targetSpace.GetComponent<Actor>();
                            chainState = 2; // select location state
                            targetSpace = null;
                            break;
                    };
                    break;
            // select location
            case 2: switch(targetSpace.tag){
                    case "Untagged": 
                            Debug.Log("Select location menu!");
                            // newAssignee.assignedLocation = selectedLocationMenu();
                            chainState = 3;
                            targetSpace = null;
                            break;
                    default: 
                            Debug.Log("Select location menu does not a appear since the location was explicity targeted.");
                            // The actors location is a Gameobject.
                            newAssignee.assignedLocation = targetSpace;
                            if(newAssignee.assignedAction == null){
                                chainState = 3;
                            } else{
                                chainState = 1;
                            }
                            
                            targetSpace = null;
                            break;
                    };
                    break;
            // select action
            case 3: switch(targetSpace.tag){
                    case "Untagged": 
                            Debug.Log("Action menu!");
                            // newAssignee.assignedAction = selectedActionMenu();
                            // newAssignee.beginAction();
                            chainState = 1;
                            targetSpace = null;
                            break;
                    default: 
                            Debug.Log("Perform action that was selected explicitly by pointer!");
                            newAssignee.assignedAction = targetSpace;
                            newAssignee.beginAction();
                            this.newAssignee = null;
                            chainState = 1;
                            targetSpace = null;
                            break;
                    };
                    break;
        }

        }
        
            
    }
}
