using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonNetworkWrapper : MonoBehaviour
{
    public static T GetCustomPropertyValue<T>(string key)
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(key))
        {
            Debug.Log((T)PhotonNetwork.CurrentRoom.CustomProperties[key]);
            return (T)PhotonNetwork.CurrentRoom.CustomProperties[key];
        }

        return default;
    }

    public static void SetCustomPropertyValue(string key, object value)
    {
        var hash = new ExitGames.Client.Photon.Hashtable
        {
            { key, value }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
