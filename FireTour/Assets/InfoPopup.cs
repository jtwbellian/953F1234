using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPopup : MonoBehaviour
{

    public GameObject info;
    public GameObject icon;

    public void Show()
    {
        info.SetActive(true);
        icon.SetActive(false);
    }

    public void Hide()
    {
        info.SetActive(false);
        icon.SetActive(true);
    }
}
