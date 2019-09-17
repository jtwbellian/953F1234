using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dataText : MonoBehaviour
{
    public enum VAR {roomCode, username}

    public TextMeshProUGUI field;
    public VAR type = VAR.roomCode;

    // Start is called before the first frame update
    private void Awake() 
    {
        if (!field)
            field = GetComponent<TextMeshProUGUI>();
            
        Refresh();
    }

    public void Refresh()
    {
        switch(type)
        {
            case VAR.roomCode:
                field.text = DataField.dataField.roomCode;
                break;

            case VAR.username:
                field.text = DataField.dataField.userName;
                break;
        }
        
    }
}
