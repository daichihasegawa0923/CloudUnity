using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    [SerializeField] Vector3 _riverForce;
    private void OnTriggerStay(Collider other)
    {
        var rig = other.GetComponent<Rigidbody>();
        if (!rig)
            return;

        rig.velocity = this._riverForce;
    }
}
