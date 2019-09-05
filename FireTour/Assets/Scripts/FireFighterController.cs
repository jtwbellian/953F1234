using UnityEngine;
using UnityEngine.AI;

public class FireFighterController : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Animator animator;

    private float maxSpeed;

    // Update is called once per frame
    void Start()
    {
     maxSpeed = agent.speed;
     agent.SetDestination(target.transform.position);
    }

    void Update()
    {
    animator.SetFloat("Speed", agent.velocity.magnitude / maxSpeed);
    }
}
