using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindHorn : MonoBehaviour
{

    public float windSpeed = 1f;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Blue_Horn_Skies.png", true);
        Gizmos.color = Color.white;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
