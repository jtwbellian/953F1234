using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// KnockDownDoor is a very loose example of defining a class since I am too lazy to actually
/// write a less abstracted example. Anyway this is the concrete class of what type of action will
/// be performed.  Let's say you add this script to an axe, that means the action that any actor
/// will perform is knocking down a door with the axe.  Why is an action on an inanimate object?
///
/// Do you shoot people with a hammer?  If you had a gun would you beat in a nail?  An actor could
/// define their action to be like that.
///
/// It is more intuitive for the object to define it's own purpose and force the actor to perform
/// it correctly.
/// </summary>
public class KnockDownDoor : DelegationAction
{

    void start(){

    }

    void update(){

    }

    /// <summary>
    /// This is the action to be performed by the performer.  The action should describe what the
    /// actor is doing, to what, and where.  Feel free to start or stop in the start and update
    /// methods. Honestly, I don't know how the actor could perform otherwise than this being
    /// called in some way in those methods.
    /// </summary>
    /// <param name="actor"></param>
    public override void startAction(GameObject actor){
        // do something with the actor object
        Debug.Log("Beginning performance with the KnockDownDoor action");
    }

    /// <summary>
    /// Stop the actor's performance of the action.  The actor will call stopPerforming which then
    /// will call this method. This method will define how the action will be stopped.
    /// </summary>
    /// <param name="actor"></param>
    public override void stopAction(GameObject actor){
        // stop in some way
        Debug.Log("Stopping performance with the KnockDownDoor action");
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