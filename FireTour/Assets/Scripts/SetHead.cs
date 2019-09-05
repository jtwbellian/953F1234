using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHead : MonoBehaviour
{

    public GameObject[] allHeads;

    public int headIndex;

    // Start is called before the first frame update
    void Start()
    {
      for (int i = 0; i < allHeads.Length; i++)
        {
            if (i == headIndex)
            {
                allHeads[i].gameObject.SetActive(true);
            }
            else
            {
                allHeads[i].gameObject.SetActive(false);
            }
        }
    }

}
