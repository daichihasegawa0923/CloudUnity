using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(this._playerPrefab.name, new Vector3(0, 4, 0), Quaternion.identity, 0);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene(0);
    }

    private void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
            Debug.LogError("this is not a master client.");

        Debug.LogFormat("PhotonNetwork:loading level : {0}",PhotonNetwork.CurrentRoom.PlayerCount);
        // PhotonNetwork.LoadLevel("RoomFor"+PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.LogFormat("OnPlayerLeftRoom():{0}", otherPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
            this.LoadArena();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.LogFormat("OnPlayerEnteredRoom():{0}", newPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
            this.LoadArena();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
