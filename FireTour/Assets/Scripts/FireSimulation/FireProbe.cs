using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider)), RequireComponent(typeof(Rigidbody))]
public class FireProbe : MonoBehaviour
{
    private const float MAX_RADIUS = 2f;

    private int num = 1;
    private bool lit = false;
    private float growRate = 2f;
    private float startRadius = 0.5f;
    private FireManager fm;
    private FXManager fx;
    private SphereCollider trigger;
    private Rigidbody rb; 
    private float updateTime = 2.5f;
    private Vector3 randomOffset;

    public FXManager.PType fireType = FXManager.PType.Flame1;
    public FXManager.PType smokeType = FXManager.PType.Smoke1;

    public bool activeOnStart = false;

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        if (activeOnStart || lit)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        fm = FireManager.GetInstance();
        fx = FXManager.GetInstance();

        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;
        trigger.radius = startRadius;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

        if (activeOnStart)
            TurnOn();

        randomOffset = new Vector3(Random.Range(-0.2f, 0.2f),  Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
    }

     [ContextMenu("Light")]
    public void TurnOn()
    {
        lit = true;
        fm.AddProbe(this);
        StartCoroutine("Burn");
    }
     [ContextMenu("Extinguish")]
    public void TurnOff()
    {
        lit = false;
        fm.RemoveProbe(this);
        StopCoroutine("Burn");
    }

    // Refresh
    public void Grow()
    {
        if (trigger.radius < MAX_RADIUS)
            trigger.radius = trigger.radius * growRate;
    }

    IEnumerator Burn()
    {
        while (lit)
        {
            randomOffset = new Vector3(Random.Range(-0.2f, 0.2f),  Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            fx.Burst(fireType, transform.position + randomOffset, fm.windHorn.windSpeed, num);
            fx.Burst(smokeType, transform.position + randomOffset, fm.windHorn.windSpeed, num);
            yield return new WaitForSeconds(updateTime);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!lit)
            return;

        var probe = other.GetComponent<FireProbe>();

        if (!probe)
            return;

        if (!probe.lit)
        {
            probe.TurnOn();
        }
    }
}
