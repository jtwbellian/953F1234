using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This input manager will poll for input, which means it will loop and sense input coming in,
/// for every manager or thing that needs input from the user.  The decision to have the managers
/// referenced here is because I would like to know which managers and etc are needing input and
/// be able to get that information in one script.
///
/// For example, tell me how many things in the scene need user input right now?  From this script
/// alone I can tell that it is only 1.  The delegation manager is the only one subscribing any
/// methods!!!
///
/// The other design is to have each manager or object reference the input manager and subscribe
/// in their respective classes. But that doesn't tell me all who have subscribed in one place so
/// I am against it.
/// </summary>
public class InputManager : MonoBehaviour
{

    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }

    // Drag and drop the delegation manager.  I don't wanna keep using findgameobject since
    // it increases load time.
    public DelegationManager delegationManager;

    public GameObject selectedTarget;
    public delegate void Selection(GameObject obj);
    public event Selection OnButtonDownSelection;


    private void Awake(){
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Start(){
        // subscribed the delegation selection to be based on when this input manager selects
        // something.
        OnButtonDownSelection += delegationManager.Selection;
    }

    void Update(){
        CheckForControllerEvents();
    }

    /// <summary>
    /// This checks for controller inputs. Why are we only doing that here?  Because the technique
    /// that Unity uses is polling.  Which get's computationally expensive if every script polls.
    ///
    /// Does the new Unity input system circumvent this?  Yes, Yes it does.  Do I know how to use
    /// it?  No, No I do not.
    ///
    /// I am using this guys code on youtube...sorta. https://youtu.be/7qsSq0zmG0s
    /// </summary>
    protected void CheckForControllerEvents(){
        // ============= Debug input and Example input!! =====================
       if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                selectedTarget = hit.collider.gameObject;
                OnButtonDownSelection(hit.collider.gameObject);
                Debug.Log("You selected the " + hit.transform.name);
            }
        }
        // ============= Debug input and Example input!! =====================

        // Oculus VR Basic Inputs


    }
}