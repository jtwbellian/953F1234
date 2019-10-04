using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSearch : DelegationAction
{
    private string targetLocation = "NA";
    private Transform entrance = null;
    private FireFighter fireFighter;

    private string title = "Search";

    public void Start()
    {
        //if (DelegationManager.Instance.menu.locations.ContainsKey(targetLocation))
       //     entrance = DelegationManager.Instance.menu.locations[targetLocation];

    }

    public override void startAction(GameObject actor)
    {
        // do something with the actor object
        
        fireFighter = actor.GetComponent<FireFighter>();
        targetLocation = fireFighter.assignedLocation.name;

        if (fireFighter)
        {
            fireFighter.controller.SetDestination(fireFighter.assignedLocation.transform);
            fireFighter.controller.onDestinationArrived += BeginSearch;
            fireFighter.charaButton.SetStatus(Status.running, "Headed to " + targetLocation);
        }
        else
        {
            Debug.Log("Error: Trying to start action but No FireFighter component found.");
        }
    }

    public void BeginSearch()
    {
        //var wall location = fireFighter.assignedLocation.GetComponent<DelegationLocation>();

        if (fireFighter.inDoorway) // If in a doorway, open the door and enter
        {
            fireFighter.charaButton.SetStatus(Status.door, "Entering From " + targetLocation);
        }
        // 

        fireFighter.controller.onDestinationArrived -= BeginSearch;
    }
    
    /// <summary>
    /// Stop the actor's performance of the action.  The actor will call stopPerforming which then
    /// will call this method. This method will define how the action will be stopped.
    /// </summary>
    /// <param name="actor"></param>
    public override void stopAction(GameObject actor)
    {
        // stop
        FireFighter fireFighter = actor.GetComponent<FireFighter>();
        
        if (fireFighter)
        {
            fireFighter.controller.CancelDestination();
        }

        fireFighter.controller.onDestinationArrived -= BeginSearch;
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
