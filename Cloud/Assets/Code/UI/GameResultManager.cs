using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    [SerializeField] private GameObject _body;
    [SerializeField] private Text _winnerName;
    [SerializeField] private Button _button;

    private void Awake()
    {
        var onClickEvent= new Button.ButtonClickedEvent();
        onClickEvent.AddListener(() =>
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        });

        this._button.onClick = onClickEvent;

        this._body.SetActive(false);
    }

    public void LeftRoom()
    {
    }
    
    public void AppearCanvas(string winnerName)
    {
        this._body.SetActive(true);
        this._winnerName.text = winnerName;
    }

}
