using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;
    private bool gameStarting = false;

    public GameObject [] playButtons;

    public GameObject CancelButton;
    public TextMeshProUGUI messageField;
    public TextMeshProUGUI codeField;

    public string roomPrefix = "BinariStudios";

    private int maxPlayers = 2;
    private int attempt = 0; // attempts to create room, appended to new room

    private void Awake() 
    {
        {
            lobby = this; // quick singleton implementation
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // try to connect to photon master server
    }

    /*public void SetRoomName(string name)
    {
        roomName = name;
    }*/

#region  Networking Functions 
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the Photon Master Server.");
        messageField.text = "Connected.";
        PhotonNetwork.AutomaticallySyncScene = true;

        foreach (var button in playButtons)
        {
            button.SetActive(true);
        }
    }

    /* public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("No games found.");
        CreateRoom();
    }
    */

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Trying again...");
        attempt ++;
        Invoke("CreateRoom", 0.2f);
    }
    void CreateRoom()
    {
        string roomCode = GenerateRoomCode();
        RoomOptions roomOps = new RoomOptions(){IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayers};
        PhotonNetwork.CreateRoom(roomPrefix + roomCode, roomOps);
        codeField.text = roomCode;
        Debug.Log("New session " + roomCode);
    }

#endregion

    [ContextMenu("GenerateCode")]
    public string GenerateRoomCode()
    {
        int num = Random.Range(100000, 999999);

        int digitC1 = (num / 10000);
        int digitC2 = (num - ((num/10000) * 10000)) / 100;
        char c1 = (char)((int)'A' + digitC1 % ((int)'Z' - (int)'A'));
        char c2 = (char)((int)'A' + digitC2 % ((int)'Z' - (int)'A'));

        string code = c1.ToString() + c2.ToString() + (num - ((num/10000) * 10000)).ToString();

        return code;
    }

    public void OnJoin(string room)
    {
        if (gameStarting)
            return;
        
        //PlayButton.SetActive(false);
        CancelButton.SetActive(true);

        PhotonNetwork.JoinRoom(roomPrefix + room);
        //PhotonNetwork.JoinRandomRoom();

        gameStarting = true;
    }

    public void OnHost()
    {
        if (gameStarting)
            return;

        CreateRoom();
    }

    public void OnStartTraining()
    {
        gameStarting = true;
    }

    public void OnCancelButtonPressed()
    {
        CancelButton.SetActive(false);

        //foreach (var button in playButtons)
        //{
        //    button.SetActive(true);
        //}

        gameStarting = false;
        //PhotonNetwork.LeaveRoom();
    }

}
