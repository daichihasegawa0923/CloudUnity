using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _idText;
    [SerializeField] private GameObject[] _respornPositions;
    [SerializeField] private ControledCharacter _controledCharacter;
    [SerializeField] private Text _startText;
    // Start is called before the first frame update
    void Start()
    {
        var chara = PhotonNetwork.Instantiate(PlaySetting.playCharacterResoucesName,
            _respornPositions[PhotonNetwork.CurrentRoom.PlayerCount - 1].transform.position,
            Quaternion.identity,
            0);

        this._controledCharacter = chara.GetComponent<ControledCharacter>();

        // 対戦の時だけ、全員参加するまで待機する
        if(PlaySetting.gameMode == PlaySetting.GameMode.battle)
            StartCoroutine("WaitUser");

        this._idText.text = PhotonNetwork.CurrentRoom.Name;
    }

    IEnumerator WaitUser()
    {
        this._controledCharacter.enabled = false;
        this._startText.text = "ほかのユーザーの参加を待っています。";
        this._startText.gameObject.SetActive(true);
        while (true)
        {
            yield return new WaitForSeconds(0.50f);
            var joinNum = PhotonNetwork.CurrentRoom.PlayerCount;
            var maxNum = PhotonNetwork.CurrentRoom.MaxPlayers;

            if (joinNum == maxNum)
            {
                this._controledCharacter.enabled = true;
                break;
            }
        }

        this._startText.text = "スタート";
        var scale = this._startText.rectTransform.localScale;
        this._startText.rectTransform.localScale = Vector3.zero;
        this._startText.rectTransform.DOScale(scale, 1.5f);
        yield return new WaitForSeconds(2.0f);
        this._startText.gameObject.SetActive(false);
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
