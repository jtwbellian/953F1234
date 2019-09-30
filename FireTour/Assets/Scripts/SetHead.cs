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
        UpdateHead();
    }

    // Dan, moved your code from the start function so I could use this in editor
    [ContextMenu("Update Head")]
    public void UpdateHead()
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
