using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class Laucher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _gameVersion = "1";
    [SerializeField] private GameObject _progressPanel;
    [SerializeField] private GameObject _controlPanel;

    [SerializeField] private byte _maxPlayerPerRoom = 4;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster():success");
        var result = PhotonNetwork.JoinRandomRoom();
        Debug.Log("PhotonNetwork.JoinRandomRoom():" + result);
        StartCoroutine("Test");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルーム参加失敗");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = this._maxPlayerPerRoom});
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _progressPanel.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ルーム参加成功");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            PhotonNetwork.LoadLevel("RoomFor1");
    }

    public void Connect()
    {
        _progressPanel.SetActive(true);
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.JoinRandomRoom();
        else
        {
            PhotonNetwork.GameVersion = this._gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    IEnumerator Test()
    {
        while (true)
        {
            Debug.Log(PhotonNetwork.CurrentRoom);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
