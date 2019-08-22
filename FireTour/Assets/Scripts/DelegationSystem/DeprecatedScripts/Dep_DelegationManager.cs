// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// // DEPRECATED!!!!


// /// <summary>
// /// The MasterController is the engine of the delegation system. It assigns
// /// the actions, locations, and selects the DelegationActor.
// /// </summary>
// private class Dep_DelegationManager : MonoBehaviour
// {
//     // only one MasterController at a time.
//     private static Dep_DelegationManager _instance;
//     public static Dep_DelegationManager Instance { get { return _instance; } }
//     public static delegate void autoAssign();
//     public static event autoAssign AutoAssign;

//     // chainState is the current state that the player is in since there is a
//     // chain of events.  First state is selecting the character, then location,
//     // then action.
//     public int chainState;
//     public GameObject targetSpace;
//     public DelegationActor[] actors;
//     public DelegationActor newAssignee;

//     private void Awake()
//     {
//         if (_instance != null && _instance != this)
//         {
//             Destroy(this.gameObject);
//         } else {
//             _instance = this;
//         }
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         chainState = 1;
//         actors = GameObject.FindObjectsOfType<DelegationActor>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(targetSpace != null){
//             switch(chainState){
//                 // select character
//                 case 1: SelectCharacter(targetSpace.tag);
//                         break;
//                 // select location
//                 case 2: SelectLocation(targetSpace.tag);
//                         break;
//                 // select action
//                 case 3:
//                         break;
//                     }
//         }
//     }

//     /// <summary>
//     /// Given the name of the selected target while in the "Select Character"
//     /// state show the menu, explicitly select a character, or perform a
//     /// a special case.
//     /// </summary>
//     /// <param name="targetName"></param>
//     public void SelectCharacter(string targetName){
//         switch(targetName){
//             case "Untagged":
//                     Debug.Log("Character select menu!");
//                     // newAssignee = selectedCharacterMenu();
//                     // check if there is an actual selection first.
//                     this.chainState = 2;
//                     this.targetSpace = null;
//                     break;
//             case "location":
//                     Debug.Log("Auto assign character and select location.");
//                     this.newAssignee = this.AutoAssign();
//                     Debug.Log(newAssignee.actorName);
//                     this.newAssignee.assignedLocation = targetSpace;
//                     this.chainState = 3; // select action state
//                     this.targetSpace = null;
//                     break;
//             case "action":
//                     Debug.Log("Auto assign idle character and then move perform action.");
//                     this.newAssignee = this.AutoAssign();
//                     Debug.Log(newAssignee.actorName);
//                     this.newAssignee.setAction(targetSpace.GetComponent<DelegationAction>());
//                     this.chainState = 2; // select location state
//                     break;
//             default:
//                     Debug.Log("Character select menu does not appear since a character was directly targeted.");
//                     this.newAssignee = targetSpace.GetComponent<DelegationActor>();
//                     this.chainState = 2; // select location state
//                     this.targetSpace = null;
//                     break;
//         };
//     }

//     public void SelectLocation(string targetName){
//         switch(targetName){
//             case "Untagged":
//                     Debug.Log("Select location menu!");
//                     // newAssignee.assignedLocation = selectedLocationMenu();
//                     this.chainState = 3;
//                     this.targetSpace = null;
//                     break;
//             default:
//                     Debug.Log("Select location menu does not a appear since the location was explicity targeted.");
//                     // The actors location is a Gameobject.
//                     this.newAssignee.assignedLocation = targetSpace;
//                     if(this.newAssignee.assignedAction == null){
//                         this.chainState = 3;
//                     } else{
//                         this.chainState = 1;
//                     }

//                     this.targetSpace = null;
//                     break;
//             };
//     }

//     public void SelectionAction(string targetName){
//         switch(targetSpace.tag){
//             case "Untagged":
//                     Debug.Log("Action menu!");
//                     // this.newAssignee.assignedAction = selectedActionMenu();
//                     // this.newAssignee.beginAction();
//                     this.chainState = 1;
//                     this.targetSpace = null;
//                     break;
//             default:
//                     Debug.Log("Perform action that was selected explicitly by pointer!");
//                     this.newAssignee.assignedAction = targetSpace;
//                     this.newAssignee.beginAction();
//                     this.chainState = 1;
//                     break;
//             };
//     }
// }
