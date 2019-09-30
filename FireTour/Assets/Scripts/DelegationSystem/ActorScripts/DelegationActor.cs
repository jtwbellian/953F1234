using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The DelegationActor is the character or object that gets assigned tasks and delegated.
/// This actor performs the actions that are described in the DelegationAction class. Those are
/// user defined, therefore for this actor to perform an action YOU as the client developer must
/// create Actions that inherit the DelegationAction class.
/// </summary>
public class DelegationActor : MonoBehaviour
{
    public DelegationManager delMnger;
    public ActorManager actorMnger;
    public string actorName;
    public int uid; // uid = Unique Identifier
    public GameObject assignedLocation;
    public GameObject assignedAction;


    void Start()
    {
        delMnger = GameObject.FindObjectOfType<DelegationManager>();
        actorMnger = GameObject.FindObjectOfType<ActorManager>();

        // use the globale game event system to do something.
        // globalEventSystem.Quit += resetAssignments;

        // Added init function for inheriting actors
        Init();
    }    
    
    public virtual void Init(){}

    /// <summary>
    /// Creates a range of values from 0-2. 2 being completely ready and 1 not being ready at
    /// all. This has the purpose of notifying the user that someone has been assigned stuff
    /// but is not done.
    /// </summary>
    /// <returns></returns>
    public int isReady(){
        int ready = 0;
        ready += isAssignedAction() ? 1 : 0;
        ready += isAssignedLocation() ? 1 : 0;
        return ready;
    }

    public void setAction(GameObject action){
        this.assignedAction = action;
    }

    public void setLocation(GameObject loc){
        this.assignedLocation = loc;
    }


    public bool isAssignedLocation(){
        if(this.assignedLocation == null){
            return false;
        }
        return true;
    }

    public bool isAssignedAction(){
        if(this.assignedAction == null){
            return false;
        }
        return true;
    }

    /// <summary>
    /// This actor is registered idle.
    /// </summary>
    public void becameIdle(){
        this.actorMnger.idleMap.Add(this.uid, this);
    }

    /// <summary>
    /// This actor unregistered as idle.
    /// </summary>
    public void becameActive(){
        this.actorMnger.idleMap.Remove(this.uid);
    }

    /// <summary>
    /// This actor will begin the performance that is laid out in the DelegationAction!  So
    /// defining the actions that can be performed as different classes is a must.  Look in the
    /// ActionScripts folder and define them there.
    /// </summary>
    public void beginPerformance(){
        // do not remove this code.  This makes them not be idle.
        this.actorMnger.idleMap.Remove(this.uid);
        // perform with the assigned action.  However this requires moving to the action and doing
        // something.  This will need to implemented in the specific action!  We pass the
        // information about the actor that is performing the action so that the action can use
        // that information. Such as getting the location of what action is to be performed with.
        this.assignedAction.GetComponent<DelegationAction>().startAction(this.gameObject);
    }

    /// <summary>
    /// Stops the actor performing the action and makes them idle...AKA available for
    /// auto assignment
    /// </summary>
    public void stopPerforming(){
        this.assignedAction.GetComponent<DelegationAction>().stopAction(this.gameObject);
        this.becameIdle();
    }

    /// <summary>
    /// Pause the performance of the actor with the assigned action.
    /// </summary>
    public void pausePerformance(){
        this.assignedAction.GetComponent<DelegationAction>().pauseAction(this.gameObject);
        this.becameIdle();
    }

    /// <summary>
    ///  Resume a paused performance of the actor with the assigned action.
    /// </summary>
    public void resumePerformance(){
        this.assignedAction.GetComponent<DelegationAction>().resumeAction(this.gameObject);
        this.becameActive();
    }

    public void performanceCompleted(){
        this.resetAssignments();
        this.stopPerforming();
    }

    /// <summary>
    /// Reset the actor.
    /// </summary>
    public void resetAssignments(){
        this.assignedAction = null;
        this.assignedLocation = null;
    }
}