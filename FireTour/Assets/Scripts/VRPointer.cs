using UnityEngine;
using System.Collections;

public class VRPointer : MonoBehaviour {

    private GameObject hand;
    private float maxDist = 2000f;
    private VRButton lastButton;

    public GameObject primaryHand;
    public GameObject secondaryHand;
    public GameObject cursor;
    public GameObject laser;
    public Transform playerTransform;
    
    // Use this for initialization
    void Start () 
    {
        cursor = Instantiate(cursor);
        laser.SetActive(false);
        hand = primaryHand;
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
        RaycastHit hit;
        var layerMask = 1 << 5;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out hit, maxDist, layerMask ))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 10f, true);

            if (cursor.activeSelf)
            {
                cursor.transform.position = hit.point;
                Vector2 rotVector = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                float angle = Mathf.Atan2(rotVector.x, rotVector.y);
                cursor.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
            else
            {
                cursor.SetActive(true);
                laser.SetActive(true);
            }

            var button = hit.transform.GetComponent<VRButton>();

            // Hover button
            if (button && (lastButton == null || lastButton != button))
            {
                button.SetHover(true);
                lastButton = button;
            }
            // unhover last button
            if (!button && lastButton != null)
            {
                lastButton.SetHover(false);
                lastButton = null;
            }
            
            /* 
             if (OVRInput.GetDown(OVRInput.Button.Four))
             {
                 Teleport();//(cursor.transform.position, cursor.transform.rotation);
             }*/
        }
        else
        {
            // unhover last button
            if (lastButton)
            {
                lastButton.SetHover(false);
                lastButton = null;
            }
            if (cursor.activeSelf)
            {
                cursor.SetActive(false);
                laser.SetActive(false);
            }
        }

        CheckControllerInput();
    }

    public void CheckControllerInput()
    {
        //
        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            if (hand == secondaryHand)
            {
                hand = primaryHand;
                laser.transform.SetParent(hand.transform);
                laser.transform.localPosition = Vector3.zero;
                laser.transform.localRotation = Quaternion.identity;
            }
            else if (lastButton)
            {
                lastButton.Click();
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            if (hand == primaryHand)
            {
                hand = secondaryHand;
                laser.transform.SetParent(hand.transform);
                laser.transform.localPosition = Vector3.zero;
                laser.transform.localRotation = Quaternion.identity;
            }
            else if (lastButton)
            {
                lastButton.Click();
            }
        }
    }

}
