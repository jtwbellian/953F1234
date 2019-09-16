using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the super class the Actions will inherit from!  You must define the actions more
/// in the subclasses.  The actions methods will receive the information about the game object
/// that has the action assigned to it.  The action that the actor will perform will need to be
/// implemented by YOU the client developer.  This means where to go, what animations to play, and
/// etc will be defined in the action.
///
/// ALSO please keep the actionss you make in the ActionScript folder.
/// </summary>
public abstract class DelegationAction: MonoBehaviour
{
    public abstract void startAction(GameObject obj);
    public abstract void stopAction(GameObject obj);
    public abstract void pauseAction(GameObject obj);
    public abstract void resumeAction(GameObject obj);
}