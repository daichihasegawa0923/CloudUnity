using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrippedBase : MonoBehaviour
{
    [SerializeField] protected List<Collider> _colliders;

    public virtual void Gripped(Grip grip)
    {
        grip.HJoint = gameObject.AddComponent<HingeJoint>();
        grip.HJoint.connectedBody = grip.gameObject.GetComponent<Rigidbody>();
        grip.HJoint.useLimits = true;
        this._colliders.ForEach(c => c.enabled = false);
    }

    public virtual void Released(Grip grip)
    {
        Destroy(grip.HJoint);
        this._colliders.ForEach(c => c.enabled = true);
    }
}
