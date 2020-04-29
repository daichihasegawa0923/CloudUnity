using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrippedBase : MonoBehaviour
{
    [SerializeField] protected List<Collider> _colliders;
    protected Rigidbody _rigidbody;
    public virtual void Gripped(Grip grip)
    {
        transform.parent = grip.transform;
        this._rigidbody = GetComponent<Rigidbody>();
        Destroy(this._rigidbody);
        this._colliders.ForEach(c => c.enabled = false);
    }

    public virtual void Released(Grip grip)
    {
        transform.parent = null;
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody = this._rigidbody;
        this._colliders.ForEach(c => c.enabled = true);
    }
}
