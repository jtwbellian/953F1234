using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public static int doorsOpen = 0;
    private Animator animator;

    public GameObject textMeshFront;

    public GameObject textMeshBack;

    private string text; 

    public GameObject[] interior;

    public GameObject[] exterior;

    public Interior interiorBoundary;
    public SphereCollider handleCollider;
    public GameObject hintUI;
    public Transform entryPoint;
    public GameObject rootDoor;

    private bool doorIsOpen;
    private bool invertActivation = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        SetDoorText("Squeeze Grip to Open Door");
    }

    void SetDoorText(string text)
    {
        TextMeshPro frontText = textMeshFront.GetComponent<TextMeshPro>();
        TextMeshPro backText = textMeshBack.GetComponent<TextMeshPro>();

        frontText.SetText(text);
        backText.SetText(text);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        // Only if the tag is player and is a spherecollider ( Not body Capsule )
        if (other.tag == "Player" && other.GetType() == typeof(CapsuleCollider))
        {
            //SphereCollider sphere = other as SphereCollider; 

            /* //Return if staying in outer trigger
            if (Vector3.Distance(other.transform.position, transform.TransformPoint(handleCollider.center)) > sphere.radius + handleCollider.radius)
            {
                return;
            }
            */

            // otherwise hand must be over inner trigger

            if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
            {
                if (doorIsOpen)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            }
        }
    }

    [ContextMenu("Open")]
    public void Open()
    {
        DoorIsOpen();
        rootDoor.GetComponent<Animator>().SetBool("Open", true);
    }
    
    [ContextMenu("Close")]
    public void Close()
    {
        DoorIsShut();
        rootDoor.GetComponent<Animator>().SetBool("Open", false);
    }

    void OnTriggerEnter(Collider Other)
    {
        if (hintUI !=null && Other.tag == "Player")
        {
                if (invertActivation)
                {
                    if (hintUI.activeSelf)
                        hintUI.gameObject.SetActive(false);
                }  
                else
                {
                    if (!hintUI.activeSelf)
                        hintUI.gameObject.SetActive(true);
                }   
        }
    }

    private void OnTriggerExit(Collider Other)
    {
        if (hintUI !=null && Other.tag == "Player")
        {
            if (invertActivation)
            {
                if (!hintUI.activeSelf)
                    hintUI.gameObject.SetActive(true);
            }  
            else
            {
                if (hintUI.activeSelf)
                    hintUI.gameObject.SetActive(false);
            }   
        }
    }

    void SetInteriorState(bool state)
    {
        for(int i = 0; i <interior.Length; i++)
        {
            interior[i].SetActive(state);
        }
    }

    void SetExteriorState(bool state)
    {
        for(int i = 0; i <exterior.Length; i++)
        {
            exterior[i].SetActive(state);
        }
    }

    private bool GetInteriorState()
    {
        if (interior[0] != null && interior[0].activeSelf)
            return (true);
        else 
            return (false);
    }
    private bool GetExteriorState()
    {
        if (exterior[0] != null && exterior[0].activeSelf)
            return (true);
        else 
            return (false);
    }

    public void DoorIsShut()
    {
        Door.doorsOpen --;
        SetDoorText("Squeeze Grip to Open Door");
        doorIsOpen = false;
        if (Door.doorsOpen == 0)
        {
            if (interiorBoundary.playerIsInside == true)
            {
                if (!GetInteriorState())
                    SetInteriorState(true);

                if (GetExteriorState())
                    SetExteriorState(false);
            }
            else
            {
                if (GetInteriorState())
                    SetInteriorState(false);

                if (!GetExteriorState())
                    SetExteriorState(true);
            }
        }
        
    }


    public void DoorIsOpen()
    {
        Door.doorsOpen ++;
        SetDoorText("Squeeze Grip to Close Door");
        doorIsOpen = true;
        if (!GetInteriorState())
            SetInteriorState(true);

        if (!GetExteriorState())
            SetExteriorState(true);
    }
}
