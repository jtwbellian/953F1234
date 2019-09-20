using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataField : MonoBehaviour
{
    public static DataField dataField;
    //public enum roomData {AccessCode, UserName};

    public string roomCode = "XX000";
    public string userName = "UNKNOWN USER";

    private void Awake() 
    {
        if (DataField.dataField != null)
        {
            Destroy(this.gameObject);
        }

        dataField = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }
}
