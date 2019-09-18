﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider)), RequireComponent(typeof(Rigidbody))]
public class FireProbe : MonoBehaviour
{
    private const float MAX_RADIUS = 2f;

    private int num = 1;
    private bool lit = false;
    private const float growRate = 2f;
    private const float startRadius = 0.5f;
    private FireManager fm;
    private FXManager fx;
    public SphereCollider trigger;
    private Rigidbody rb; 
    private float updateTime = 2.5f;
    private Vector3 randomOffset;

    //Calculuate number of times a probe can expand before reaching MAX_RADIUS
    private static int shellCount = Mathf.RoundToInt(Mathf.Log(MAX_RADIUS / startRadius) / Mathf.Log(growRate)); 

    public int shellIndex = 0;

    public List<int>[] VertexGroup = new List<int>[shellCount];

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
    public void Start()
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

        randomOffset = new Vector3(0, 0, 0); //Vector3(Random.Range(-0.05f, 0.05f),  Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
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
    {//Debug.Log("I have grown.");
        if (trigger.radius < MAX_RADIUS)
        {
            List<int> shellList = new List<int>(VertexGroup[shellIndex]);
            //Debug.Log("I have assigned my shell and it contains..." + shellList.ToString());
            if (shellList.Count > 0)
            {
                Debug.Log("Shell List is NOT null. Proceeding...");
                for (int i = 0; i < shellList.Count; ++i)
                {
                    //shellList contains several integers that each reference a particular vertex in the mesh.
                    //This for loop is cycling through each of these integers and matching them up with their 
                    //equivalent counterparts in the array of mesh vertex colors.  
                    //if (lit)
                        Debug.Log("Attempting to change color...");
                        fm.SetVertexColor(shellList[i], Color.black);
                }
            }
            //grow the probe
            trigger.radius = trigger.radius * growRate;
            //prep the next shell for the next Refresh. Put these next two lines back in once multiple shells are baked
            //if (shellIndex<shellCount)
                //shellIndex+=1;
        }

    }

    IEnumerator Burn()
    {
        while (lit)
        {
            //randomOffset = new Vector3(Random.Range(-0.2f, 0.2f),  Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
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
