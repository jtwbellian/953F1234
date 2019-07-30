using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Only one InputManager at a time
    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }
    public MasterController delegationController; 
    RaycastHit hit;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Start(){
        delegationController = GameObject.FindObjectOfType<MasterController>();
    }
     void Update()
    {
        // This will need to be swapped for the VR controller.
        if (Input.GetMouseButtonDown(0)){

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)){
                delegationController.targetSpace = hit.collider.gameObject;
                Debug.Log(hit.collider.gameObject.tag);
            }
        }
    }
}