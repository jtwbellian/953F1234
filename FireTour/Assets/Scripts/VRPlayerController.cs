using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : MonoBehaviour//OVRPlayerController
{
    public Transform trackingPoint;
    public float speed = 0.4f;
    
    private OVRScreenFade fade;
    private CharacterController character;

    #region viewControl
    private bool ReadyToSnapTurn = false;
    private Vector3 euler;
    private Vector3 lastPos; 

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fade = GetComponentInChildren<OVRScreenFade>();
        character = GetComponent<CharacterController>();
        character.SimpleMove(Vector3 .forward  * 0);
    }

    // Update is called once per frame
    void Update()
    {
        character.enabled = false;
        character.height = trackingPoint.localPosition.y;
        character.center = trackingPoint.transform.localPosition - (Vector3.up * character.height) / 2f;
        character.enabled = true;
        
        // Walk
        //Vector2 stickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        //character.SimpleMove((trackingPoint.forward  * stickInput.y * speed) + (trackingPoint.right * stickInput.x * speed));

        // Turn View Left
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            if (ReadyToSnapTurn)
            {
                ViewRatchet(-45f);
            }
        }
        else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
        {
            if (ReadyToSnapTurn)
            {
                ViewRatchet(45f);
            }
        }
        else if (ReadyToSnapTurn == false)
        {
            ReadyToSnapTurn = true;
        }
    }

    public void ViewRatchet(float amt)
    {
        euler = transform.rotation.eulerAngles;
        lastPos = trackingPoint.position;

        euler.y += amt;
        transform.rotation = Quaternion.Euler(euler);
        transform.position += lastPos - trackingPoint.position;
        ReadyToSnapTurn = false;
    }
}
