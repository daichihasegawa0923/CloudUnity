using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _gameVersion = "1";
    public static byte MaxPlayerPerRoom = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        this.Connect();
    }
    
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = this._gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            return;
        }

        PhotonNetwork.JoinLobby();

        // ルーム作成時
        if (PlaySetting.isMaster)
        {

        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("room is update");
        roomList.ForEach(r => Debug.Log(r.Name));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
