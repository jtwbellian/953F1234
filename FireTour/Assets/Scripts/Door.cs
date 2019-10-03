using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    private Animator animator;

    private GameObject textMeshFront;

    private GameObject textMeshBack;

    private string text; 

    public GameObject[] interior;

    public GameObject[] exterior;

    public Interior interiorBoundary;

    private bool doorIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        SetDoorText("Squeeze Grip /nto Open Door");
    }

    void SetDoorText(string text)
    {
        textMeshFront = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        textMeshBack = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
        
        TextMeshPro frontText = textMeshFront.GetComponent<TextMeshPro>();
        TextMeshPro backText = textMeshBack.GetComponent<TextMeshPro>();

        frontText.SetText(text);
        backText.SetText(text);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        //if (Input.GetKey(KeyCode.UpArrow))
        if (Input.GetKey(KeyCode.UpArrow) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTrackedRemote))
        {
            if (animator.GetBool("Open") == false)
            {
                animator.SetBool("Open", true);
            }
            else
            {
                animator.SetBool("Open", false);
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
        SetDoorText("Squeeze Grip /nto Open Door");
        doorIsOpen = false;
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
    public void DoorIsOpen()
    {
        SetDoorText("Squeeze Grip /nto Close Door");
        doorIsOpen = true;

        if (!GetInteriorState())
            SetInteriorState(true);
        if (!GetExteriorState())
            SetExteriorState(true);
    }
}
