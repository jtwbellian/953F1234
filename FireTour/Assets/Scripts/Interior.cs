using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interior : MonoBehaviour
{
    public bool playerIsInside;
    public GameObject[] exterior;

    // Start is called before the first frame update

    void OnTriggerEnter(Collider Other)
        {
            if (Other.gameObject.transform.tag == "Player")
            {
                if (playerIsInside == false)
                        playerIsInside = true;
                for(int i = 0; i <exterior.Length; i++)
                {
                    exterior[i].SetActive(false);
                }
            }
        }
    void OnTriggerExit(Collider Other)
        {
            if (Other.gameObject.transform.tag == "Player")
            {
                if (playerIsInside == true)
                        playerIsInside = false;
                for(int i = 0; i <exterior.Length; i++)
                {
                    exterior[i].SetActive(true);
                }
            }
        }
}
