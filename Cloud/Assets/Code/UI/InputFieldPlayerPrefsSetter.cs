using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPlayerPrefsSetter : MonoBehaviour
{
    [SerializeField] InputField _inputField;
    [SerializeField] bool _isSetInitialize;
    public string _prefsKey = "nickName";

    public void Start()
    {
        if(this._isSetInitialize)
            this._inputField.text = PlayerPrefs.GetString(_prefsKey,"no name");
    }

    public void SetPlayerPrefs(string value)
    {
        PlayerPrefs.SetString(_prefsKey, value);
        Debug.Log(PlayerPrefs.GetString(_prefsKey, ""));
    }
}
