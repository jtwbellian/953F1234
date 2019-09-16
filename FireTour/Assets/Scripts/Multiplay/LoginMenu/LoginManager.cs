using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public static LoginManager _instance = null;
    public TextMeshProUGUI [] userNameFields;
    public GameObject mainMenu;
    public GameObject menu_lobby, menu_prompt;
    public GameObject sphere;


    [ReadOnly]
    private string currentUser = "NOONE";

    private void Awake() 
    {
        if (LoginManager._instance)
        {
            Destroy(this);
        }
        else
        {
            LoginManager._instance = this;
        }
    }

    public static LoginManager GetInstance()
    {
        return _instance;
    }

    public void SetUser(string name)
    {
        foreach (var userName in userNameFields)
        {
            userName.text = name;
        }

        currentUser = name;
    }
    public string GetUser()
    {
        return currentUser;
    }

    public void ExpandSphere()
    {
        sphere.transform.localScale = new Vector3(100f, 100f, 100f);
    }

    public void ReduceSphere()
    {
        sphere.transform.localScale = new Vector3(.5f, .5f, .5f);
    }
}
