using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChooserJoin : CharacterChooser
{
    [SerializeField] protected InputField _joinRoomIdTextInput;

    [SerializeField] protected ErrorMessage _errorMessage;

    [SerializeField] protected PanelLoader _panelLoader;

    public override void StartGame()
    {
        Debug.Log(string.IsNullOrEmpty(this._joinRoomIdTextInput.text));
        if (string.IsNullOrEmpty(this._joinRoomIdTextInput.text))
        {
            _errorMessage.AppearMessage("ルームIDが入力されていません。");
            return;
        }

        this._panelLoader.ActiveOnePanelByName("NowLoading");
        base.StartGame();
    }

}
