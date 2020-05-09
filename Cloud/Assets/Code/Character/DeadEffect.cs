using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine("DeleteEffect");
    }

    IEnumerator DeleteEffect()
    {
        yield return new WaitForSeconds(3.0f);
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(gameObject);

        Destroy(gameObject);
    }
}
