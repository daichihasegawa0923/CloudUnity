using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _idText;
    [SerializeField] private GameObject[] _respornPositions;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(PlaySetting.playCharacterResoucesName,
            _respornPositions[PhotonNetwork.CurrentRoom.PlayerCount - 1].transform.position,
            Quaternion.identity,
            0);

        this._idText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("ModeChoose");
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
        PhotonNetwork.Disconnect();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
