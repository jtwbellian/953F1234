using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActStretchLine : DelegationAction
{
    public Transform entrance;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void startAction(GameObject actor){
        // do something with the actor object
        
        FireFighter fireFighter = actor.GetComponent<FireFighter>();
        
        if (fireFighter)
        {
            fireFighter.controller.SetDestination(entrance);
        }
        else
        {
            Debug.Log("Error: Trying to start action but No FireFighter component found.");
        }
    }
    
    /// <summary>
    /// Stop the actor's performance of the action.  The actor will call stopPerforming which then
    /// will call this method. This method will define how the action will be stopped.
    /// </summary>
    /// <param name="actor"></param>
    public override void stopAction(GameObject actor){
        // stop
        FireFighter fireFighter = actor.GetComponent<FireFighter>();
        
        if (fireFighter)
        {
            fireFighter.controller.CancelDestination();
        }
    }

    /// <summary>
    /// The actor will call this from its pausePerformance method...however the pausePerformance
    /// method isn't used anywhere. It is just defined, this is for future use, probably will need
    /// this.
    /// </summary>
    /// <param name="actor"></param>
    public override void pauseAction(GameObject actor){
        // pause in some way
    }

    /// <summary>
    /// The actor will call this from its resumePerformance method...however the resumePerformance
    /// method isn't used anywhere either.  This is just for future use, probably will need this.
    /// </summary>
    /// <param name="actor"></param>
    public override void resumeAction(GameObject actor){
        // resume in some way
    }

}
