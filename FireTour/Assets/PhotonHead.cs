using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PhotonHead : MonoBehaviour
{
    PhotonView view;
    public TextMeshProUGUI nametag;

    private float maxDist = 500f;
    private InfoPopup lastInfo = null;
    
    void Awake()
    {
        if (!view)
            view = GetComponent<PhotonView>();
    }

    public void SetName(string name)
    {
        view.RPC("RPC_SetName", RpcTarget.AllBuffered, name);
    }

    private void Update() 
    {
        RaycastHit hit;
        var layerMask = LayerMask.GetMask("UI");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * -1), out hit, maxDist, layerMask ))
        {
            var info = hit.transform.gameObject.GetComponentInChildren<InfoPopup>();
            Debug.Log("Hit " + hit.transform.ToString() + ", info " + info);


            if (info)
            {
                // Hover info
                if (info && (lastInfo == null || lastInfo != info))
                {
                    info.Show();
                    lastInfo = info;
                    Debug.Log("Showing info");
                }
                // unhover last button
                /* if (!info && lastInfo != null)
                {
                    info.Hide();
                    lastInfo = null;
                }*/
            }
            else
            {
                // unhover last button
                if (lastInfo)
                {
                    lastInfo.Hide();
                    lastInfo = null;
                }
            }
        }
        else if (lastInfo)
        {
            lastInfo.Hide();
            lastInfo = null;
        }
    }

    [PunRPC]
    public void RPC_SetName(string name)
    {
        nametag.text = name;
    }
}
