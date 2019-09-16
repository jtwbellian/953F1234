// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Actor : MonoBehaviour
// {
//     MasterController masterController;
//     public string actorName;
//     public bool isIdle = true;
//     public bool auto = false;
//     public GameObject assignedLocation;
//     public GameObject assignedAction;

//     void Start()
//     {
//         masterController = GameObject.FindObjectOfType<MasterController>();
//         EventManager.AutoAssignEvent += autoAssign;
//     }

//     public void beginAction(){

//         StartCoroutine(test());
//     }

//     IEnumerator test(){
//         Debug.Log("Begin Action");
//         yield return new WaitForSeconds(2);
//         assignedAction = null;
//         assignedLocation = null;
//     }

//     // subscribed method
//     void autoAssign(){
//         if(masterController.newAssignee == null){
//             masterController.newAssignee = this;
//         }
//     }
// }