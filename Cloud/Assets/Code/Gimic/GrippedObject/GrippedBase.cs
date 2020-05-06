using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrippedBase : MonoBehaviour
{
    [SerializeField] protected List<Collider> _colliders;
    protected Rigidbody _rigidbody;
    private Grip _grip;

    private Vector3 _distance;

    private void Update()
    {
        if (_grip)
            transform.position = this._grip.transform.position + this._distance;
    }

    public virtual void Gripped(Grip grip)
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.useGravity = false;
        this._colliders.ForEach(c => c.enabled = false);
        _distance = transform.position - grip.transform.position;
        this._grip = grip;
    }

    public virtual void Released(Grip grip)
    {
        this._grip = null;
        this._rigidbody.useGravity = true;
        this._colliders.ForEach(c => c.enabled = true);
    }
}
