using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _idText;
    [SerializeField] private GameObject[] _respornPositions;
    [SerializeField] private ControledCharacter _controledCharacter;
    [SerializeField] private Text _startText;
    [SerializeField] private GameResultManager _gameResultManager;

    private bool _isStartOnBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        // バトルモード設定
        if (PhotonNetwork.IsMasterClient && PlaySetting.gameMode == PlaySetting.GameMode.battle)
        {
            PhotonNetworkWrapper.SetCustomPropertyValue("game_mode", "battle");
        }else
        {
            var gameMode = PhotonNetworkWrapper.GetCustomPropertyValue<string>("game_mode");
            if (gameMode == "battle")
                PlaySetting.gameMode = PlaySetting.GameMode.battle;
        }

        var chara = PhotonNetwork.Instantiate(PlaySetting.playCharacterResoucesName,
            _respornPositions[PhotonNetwork.CurrentRoom.PlayerCount - 1].transform.position,
            Quaternion.identity,
            0);

        this._controledCharacter = chara.GetComponent<ControledCharacter>();

        this._idText.text = PhotonNetwork.CurrentRoom.Name;

        // バトルモード時の設定
        this.InitializeBattleModeGame();
    }

    private void InitializeBattleModeGame()
    {
        if (PlaySetting.gameMode != PlaySetting.GameMode.battle)
            return;

        // 対戦の時だけ、全員参加するまで待機する
        WaitUser();

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            StartGameOnBattle();
        }
    }

    private void StartGameOnBattle()
    {
        this._isStartOnBattle = true;
        this._controledCharacter.enabled = true;
        this._startText.text = "スタート";
        var scale = this._startText.rectTransform.localScale;
        this._startText.rectTransform.localScale = Vector3.zero;
        this._startText.rectTransform.DOScale(scale, 1.5f);
        Invoke("DeleteStartText", 2.0f);
    }

    private void DeleteStartText()
    {
        this._startText.gameObject.SetActive(false);
    }

    private void  WaitUser()
    {
        this._controledCharacter.enabled = false;
        this._startText.text = "ほかのユーザーの参加を待っています。";
        this._startText.gameObject.SetActive(true);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("更新あり：" + propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("dead_player_number"))
        {
            var deadPlayerNumber = (int)propertiesThatChanged["dead_player_number"];
            var onlyOneAlive = deadPlayerNumber == ((int)PhotonNetwork.CurrentRoom.PlayerCount - 1);

            if(this._controledCharacter != null)
            {
                PhotonNetworkWrapper.SetCustomPropertyValue("winner_name", PhotonNetwork.NickName);
            }
        }

        if (propertiesThatChanged.ContainsKey("winner_name"))
        {
            Debug.Log("勝負あり");
            this._gameResultManager.AppearCanvas((string)propertiesThatChanged["winner_name"]);
        }
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
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.LogFormat("OnPlayerLeftRoom():{0}", otherPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
            this.LoadArena();

        // バトルモード設定時
        if (this._isStartOnBattle)
            PhotonNetwork.CurrentRoom.MaxPlayers = (byte)((int)PhotonNetwork.CurrentRoom.MaxPlayers - 1);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.LogFormat("OnPlayerEnteredRoom():{0}", newPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
            this.LoadArena();

        // バトルモード設定時
        if (PlaySetting.gameMode != PlaySetting.GameMode.battle)
            return;

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            StartGameOnBattle();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }
}
