using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
using Photon;

public class PhotonPlayer : MonoBehaviour
{
    PhotonView PV;
    private AvatarController avController;
    public GameObject playerController;
    private AvatarParts parts;
    public string myName = "";
    

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log("PV set to " + PV);

        if (PV.IsMine)
        {
            var handL = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CustomHandLeft"),
                                                Vector3.zero, 
                                                Quaternion.identity, 0);
            var handR = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CustomHandRight"),
                                    Vector3.zero, 
                                    Quaternion.identity, 0);
            var head = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Head"),
                                    Vector3.zero, 
                                    Quaternion.identity, 0);

            var player = Instantiate(playerController);

            Debug.Log("created player object: " + player);

            avController = player.GetComponent<AvatarController>();

            head.transform.SetParent(avController.headTarget);

            PhotonHead phead = head.GetComponent<PhotonHead>();

            phead.SetName(myName);

            POVRGrabber grabberL = handL.GetComponent<POVRGrabber>();
            POVRGrabber grabberR = handR.GetComponent<POVRGrabber>();

            grabberL.SetParentTransform(player.transform);
            grabberR.SetParentTransform(player.transform);

/*
            parts = myAvatar.GetComponent<AvatarParts>();


            //PandaController panda = player.GetComponent<PandaController>();
            
            parts.Head.SetParent(avController.headTarget.transform);
            parts.Head.localRotation = Quaternion.identity;
            parts.Head.localPosition = Vector3.zero;

            parts.RHand.SetParent(avController.rhandTarget.transform);
            parts.RHand.localRotation = Quaternion.identity;
            parts.RHand.localPosition = Vector3.zero;

            parts.LHand.SetParent(avController.lhandTarget.transform);
            parts.LHand.localRotation = Quaternion.identity;
            parts.LHand.localPosition = Vector3.zero;
            */
        }
    }

}
