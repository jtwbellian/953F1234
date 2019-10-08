using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICircleFill : MonoBehaviour
{
    public Image  front;
    public Image  back;
    // Start is called before the first frame update
    void Start()
    {
        SetFillAmount(1f);
    }

    public void SetRing(bool active)
    {
        front.gameObject.SetActive(active);

        if (back)
            back.gameObject.SetActive(active);
    }

    public void SetFillAmount(float amt)
    {
        front.fillAmount = amt;

        if (back)
            back.fillAmount = 1 - amt;
    }
}
