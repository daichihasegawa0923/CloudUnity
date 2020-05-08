using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _gameVersion = "1";
    public static byte MaxPlayerPerRoom = 2;
    [SerializeField] private float _timeoutMaxTime = 30.0f;

    [SerializeField] private List<RoomInfo> _roomInfos;

    [SerializeField] private InputField _userNameTextInput;
    [SerializeField] private InputField _roomIdNameTextInput;

    [SerializeField] private ErrorMessage _errorMessage;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("OnConnectedToMaster");
    }
    
    IEnumerator CountTimeOutJoinIn()
    {
        yield return new WaitForSeconds(this._timeoutMaxTime);
        this._errorMessage.AppearMessage("ルームの参加に失敗しました。：timeout");
    }

    public void Connect()
    {
        StartCoroutine("ConnectAsync");
    }

    IEnumerator ConnectAsync()
    {
        yield return new WaitForSeconds(2.5f);

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = this._gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーへ参加");
        var nickName = string.IsNullOrEmpty(_userNameTextInput.text) ? "no name" : _userNameTextInput.text;
        PhotonNetwork.NickName = nickName;

        // ルーム参加時
        if (!PlaySetting.isMaster)
        {
            StartCoroutine("CountTimeOutJoinIn");
            Debug.Log(PlaySetting.isMaster);
            PhotonNetwork.JoinRoom(_roomIdNameTextInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        PhotonNetwork.LoadLevel(PlaySetting.playSceneName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
        this._errorMessage.AppearMessage(message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        this._errorMessage.AppearMessage(message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("room is update");
        this._roomInfos = roomList;
        // ルーム作成時
        if (PlaySetting.isMaster)
        {
            var roomNumber = this._roomInfos != null ? this._roomInfos.Count.ToString() : "0";
            PhotonNetwork.CreateRoom(roomNumber,new RoomOptions() { MaxPlayers = PlaySetting.maxPlayerNum});
        }
    }
}
