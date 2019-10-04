using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interior : MonoBehaviour
{
    public bool playerIsInside;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider Other)
        {
            if (Other.gameObject.transform.tag == "Player")
            {
                if (playerIsInside == false)
                        playerIsInside = true;
            }
        }
    void OnTriggerExit(Collider Other)
        {
            if (Other.gameObject.transform.tag == "Player")
            {
                if (playerIsInside == true)
                        playerIsInside = false;
            }
        }
}
