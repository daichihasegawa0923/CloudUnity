using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonNetworkWrapper : MonoBehaviour
{
    public static T GetCustomPropertyalue<T>(string key)
    {
        foreach (var playerItem in PhotonNetwork.PlayerList)
        {
            if (playerItem.CustomProperties.ContainsKey(key))
            {
                Debug.Log((T)playerItem.CustomProperties[key]);
                return (T)playerItem.CustomProperties[key];
            }
        }

        return default;
    }

    public static void SetCustomPropertyValue(string key, object value)
    {
        var hash = new ExitGames.Client.Photon.Hashtable
        {
            { key, value }
        };
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }
}
