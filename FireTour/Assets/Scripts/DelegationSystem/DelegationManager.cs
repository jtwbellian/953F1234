using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The DelegationManager is the engine of the delegation system. It assigns
/// the actions, locations, and commnuicates with the ActorManager.
/// </summary>
public class DelegationManager : MonoBehaviour
{
    // only one DelegationManager at a time.
    private static DelegationManager _instance;
    public static DelegationManager Instance { get { return _instance; } }
    public ActorManager actorManager;
    public DelegationMenu menu;
    public int assigneeId = -1;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void start()
    {
        if (!actorManager)
            actorManager = GameObject.FindObjectOfType<ActorManager>();

        if (!menu)
            menu = GameObject.FindObjectOfType<DelegationMenu>();
    }


    /// <summary>
    /// This method is the method that needs to be used externally. The input manager should be
    /// sending selected targets to this method. This means the script where you have your inputs
    /// going through should have reference to the delegation manager and this method.
    /// </summary>
    /// <param name="target"></param>
    public void Selection(GameObject target){
        Debug.Log("Selection made for delegation system.");
        switch(target.tag){

            case "Untagged":
                    if(assigneeId == -1){
                        Debug.Log("Character select menu!");
                        // this.newAssignee = characterSelectMenuWhenAvailable();
                    }
                    else if(!actorManager.actorMap[assigneeId].isAssignedLocation()){
                        Debug.Log("Location select menu!");
                        // this.newAssignee.setLocation(locationSelectMenuWhenAvailable());
                    }
                    else if(!actorManager.actorMap[assigneeId].isAssignedAction()){
                        Debug.Log("Action select menu!");
                        // this.newAssignee.setLocation(actionSelectMenuWhenAvailable());
                    }
                    break;
            case "actor":
                    Debug.Log("Assigned Character Manually");
                    this.setAssignee(target);
                    break;
            case "location":
                    this.assignToAssignee(target);
                    break;
            case "action":
                    this.assignToAssignee(target);
                    break;
        }

        if(this.isAssigneeReady()){
            this.assigneePerforms();
        }
    }

    /// <summary>
    /// Checks if the actor assigned is ready to perform.
    /// </summary>
    /// <returns></returns>
    private bool isAssigneeReady(){
        if(this.assigneeId != -1){
            return this.actorManager.actorMap[this.assigneeId].isReady() == 2;
        }
        return false;
    }

    /// <summary>
    /// The actor begins their their performance using the assigned action and locations.  Remember
    /// to manually create Actions!!
    /// </summary>
    private void assigneePerforms(){
        Debug.Log($"Assignee with uid of {this.assigneeId} begins work.");
        // This will throw errors if actions have not been assigned!
        this.actorManager.actorMap[this.assigneeId].beginPerformance();
        this.assigneeId = -1;
    }

    private void autoAssign(){
        this.assigneeId = this.actorManager.getIdleActor().uid;
        Debug.Log("AutoAssigned " + this.assigneeId.ToString());

    }

    /// <summary>
    /// Assigns the target gameobject to the actor that has been selected for delegation!  If no
    /// actor has been selected one will be selected for you if they are available.
    /// </summary>
    /// <param name="target"></param>
    private void assignToAssignee(GameObject target){
        if(this.assigneeId == -1){
            this.autoAssign();
        }

        if(target.tag == "action"){
            this.actorManager.actorMap[this.assigneeId].setAction(target);
        } else if(target.tag == "location"){
            this.actorManager.actorMap[this.assigneeId].setLocation(target);
        }

    }

    /// <summary>
    /// On the chance that a user selects another actor during the selections, this method
    /// will remove the assigned actions and the assigned location from the previously selected
    /// actor and the new actor will need to start being assigned.
    /// </summary>
    /// <param name="target"></param>
    private void setAssignee(GameObject target){
        if(this.assigneeId != -1 && this.assigneeId != target.GetComponent<DelegationActor>().uid){
            this.actorManager.actorMap[this.assigneeId].resetAssignments();
        }
        this.assigneeId = target.GetComponent<DelegationActor>().uid;
    }

}
