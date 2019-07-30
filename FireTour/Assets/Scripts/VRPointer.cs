using UnityEngine;
using System.Collections;

public class VRPointer : MonoBehaviour {

    public GameObject cursor;
    public Transform playerTransform;
    private float maxDist = 2000f;
    
    // Use this for initialization
    void Start () {
    
    }
    
    [ContextMenu("Teleport")]
    public void Teleport()//Vector3 pos, Quaternion rot)
    {
        if (cursor.activeSelf)
        {
            playerTransform.position = cursor.transform.position;//pos;
            playerTransform.rotation = cursor.transform.rotation;//rot;
        }
    }
    
    // Update is called once per frame
    void Update () 
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDist, layerMask))
        {
            Debug.Log("Hit");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 1f, true);

            if (cursor.activeSelf)
            {
                cursor.transform.position = hit.point;
                Vector2 rotVector = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                float angle = Mathf.Atan2(rotVector.x, rotVector.y);
                cursor.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
            else
                cursor.SetActive(true);
            
             if (OVRInput.GetDown(OVRInput.Button.Four))
             {
                 Teleport();//(cursor.transform.position, cursor.transform.rotation);
             }
        }
        else
        {
            Debug.Log("No hit");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 1f, true);

            if (cursor.activeSelf)
                cursor.SetActive(false);
        }
    }
}
