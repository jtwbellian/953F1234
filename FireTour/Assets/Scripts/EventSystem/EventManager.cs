using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Static class needs no instances since this will just be references to 
    // events.
    public delegate void autoAssignMethod();
    public static event autoAssignMethod AutoAssignEvent;

    public static void autoAssign(){
        AutoAssignEvent();
    }

}
