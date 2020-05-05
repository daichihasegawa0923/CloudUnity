using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] private GameObject _messageParent;
    [SerializeField] private Text _messageText;

    public void AppearMessage(string message = null)
    {
        this._messageParent.SetActive(true);
        if (message != null)
            this.SetMessage(message);
    }

    public void DisappearMessage()
    {
        this._messageParent.SetActive(false);
    }

    public void SetMessage(string message)
    {
        this._messageText.text = message;
    }
}
