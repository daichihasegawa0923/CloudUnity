using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrippedBase : MonoBehaviourPun
{
    [SerializeField] protected List<Collider> _colliders;
    protected Rigidbody _rigidbody;
    private Grip _grip;
    [SerializeField] private string _resoucesPrefabsName;
    private Vector3 _distance;

    private void Update()
    {
        this.ChaseObject();
    }
    
    public virtual void Gripped(Grip grip)
    {
        this.photonView.RequestOwnership();
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
    
    public virtual void ChaseObject()
    {
        if(this._grip)
            transform.position = this._grip.transform.position + this._distance;
    }
}
