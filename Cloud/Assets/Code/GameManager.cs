using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _respornPositions;
    // Start is called before the first frame update
    void Start()
    {
        var choicedCharcter = GameObject.FindObjectOfType<CharacterChoice>();
        SceneManager.MoveGameObjectToScene(choicedCharcter.gameObject, SceneManager.GetActiveScene());
        var choicedCharacterInstantiated = PhotonNetwork.Instantiate(choicedCharcter.GetGameInstance().name, new Vector3(0, 40, 0), Quaternion.identity, 0);
        choicedCharacterInstantiated.transform.position = _respornPositions.transform.Find(PhotonNetwork.CurrentRoom.PlayerCount.ToString()).position;
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("ControlPanels");
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
