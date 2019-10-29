using UnityEngine;
using UnityEngine.AI;

public class FireFighterController : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;
    private Transform myDestination;

    public delegate void OnDestinationDelegate();
    public OnDestinationDelegate onDestinationArrived; 
    public Transform home;

    private float maxSpeed;

    // Update is called once per frame
    void Start()
    {
        maxSpeed = agent.speed;
    }

    // Added this function so I can change the destination through code
    public void SetDestination(Transform target)
    {
        agent.SetDestination(target.position);
        myDestination = target;
    }

    public void CancelDestination()
    {
        agent.isStopped = true;
    }

    public void GoHome()
    {
        SetDestination(home);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.transform == myDestination)
        {
            myDestination = null;

            if (onDestinationArrived != null)
            {
                onDestinationArrived.Invoke();
            }
        }
    }
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude / maxSpeed);
    }
}
