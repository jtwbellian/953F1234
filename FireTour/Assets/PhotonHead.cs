using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PhotonHead : MonoBehaviour
{
    PhotonView view;
    public TextMeshProUGUI nametag;
    
    void Awake()
    {
        if (!view)
            view = GetComponent<PhotonView>();
    }

    public void SetName(string name)
    {
        view.RPC("RPC_SetName", RpcTarget.AllBuffered, name);
    }
    
    [PunRPC]
    public void RPC_SetName(string name)
    {
        nametag.text = name;
    }
}
