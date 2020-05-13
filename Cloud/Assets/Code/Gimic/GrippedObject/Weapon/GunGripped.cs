using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGripped : WeaponGripped
{
    [SerializeField] protected Transform _gunMousePosition;
    [SerializeField] private string _burretName = "burret";
    [SerializeField] private float _burretFrequency = 0.50f;
    [SerializeField] private float _burretSpeed = 15.0f;
    public override void Gripped(Grip grip)
    {
        base.Gripped(grip);
        StartCoroutine("Shooting");

    }

    public override void Released(Grip grip)
    {
        base.Released(grip);
        StopCoroutine("Shooting");
    }

    IEnumerator Shooting()
    {
        while (this._grip)
        {
            var burret = PhotonNetwork.Instantiate(this._burretName,_gunMousePosition.position,_gunMousePosition.rotation);
            burret.GetComponent<Rigidbody>().velocity = this._grip.Character.transform.forward * this._burretSpeed;
            StartCoroutine("DestroyBurret", burret);
            yield return new WaitForSeconds(_burretFrequency);
        }
    }

    IEnumerator DestroyBurret(GameObject burret)
    {
        yield return new WaitForSeconds(2.0f);
        PhotonNetwork.Destroy(burret);
    }
}
