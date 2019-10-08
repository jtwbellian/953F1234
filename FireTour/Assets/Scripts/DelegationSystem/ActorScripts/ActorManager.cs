using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// The ActorManager is responsible for understanding what it's
/// actors are doing.  Being idle and what not.
/// </summary>
public class ActorManager : SerializedMonoBehaviour
{
    // Every other manager will have reference to the delegation manager.
    // Looks like the delegation manager only needs to be referenced by the actor manager
    // since the locations and actions don't have special needs.
    DelegationManager delMngr;

    private static ActorManager _instance;
    public static ActorManager Instance { get { return _instance; } }
    public Dictionary<int, DelegationActor> actorMap = new Dictionary<int,DelegationActor>();
    public Dictionary<int,DelegationActor> idleMap = new Dictionary<int,DelegationActor>();

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
        delMngr = GameObject.FindObjectOfType<DelegationManager>();
        foreach(var actor in GameObject.FindObjectsOfType<DelegationActor>()){
            registerActor(actor);
        }
    }


    public DelegationActor getIdleActor(){
        DelegationActor actor = this.idleMap[idleMap.Keys.First()];
        this.idleMap.Remove(idleMap.Keys.First());
        return actor;
    }

    public void registerActor(DelegationActor actor){
        this.actorMap.Add(actor.uid, actor);
        this.idleMap.Add(actor.uid, actor);
    }

    public void registerIdleActor(DelegationActor actor){
        this.idleMap.Add(actor.uid, actor);
    }

}
