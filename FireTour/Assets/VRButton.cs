using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRButton : MonoBehaviour
{
    public Image panel;
    public Color hoverColor;
    public Color defaultColor;
    public UnityEvent onPressed;

    public bool hover = false;

    public void SetHover(bool active)
    {
        if (active)
            panel.color = hoverColor;
        else
            panel.color = defaultColor;

        hover = active;
    }

    [ContextMenu("Click")]
    public void Click()
    {
        onPressed.Invoke();
    }
}
