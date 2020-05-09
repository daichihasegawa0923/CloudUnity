using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrippedBase : MonoBehaviourPun
{
    [SerializeField] protected List<Collider> _colliders;
    [SerializeField] protected Rigidbody _rigidbody;
    protected Grip _grip;
    protected Vector3 _distance;

    private void Update()
    {
        this.HandleObject();
    }
    
    public virtual void Gripped(Grip grip)
    {
        if(PhotonNetwork.IsConnected)
            this.photonView.RequestOwnership();

        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.useGravity = false;
        transform.parent = grip.transform;
        this._colliders.ForEach(c => c.enabled = false);
        _distance = transform.position - grip.transform.position;
        this._grip = grip;
    }
    

    public virtual void Released(Grip grip)
    {
        transform.parent = null;
        this._grip = null;
        this._rigidbody.useGravity = true;
        this._colliders.ForEach(c => c.enabled = true);
    }

    protected virtual void HandleObject()
    {
        if (this._grip)
        {
            // 位置調整
            var dPosition = this._grip.transform.up;

            this.transform.position = this._grip.transform.position + dPosition;
            this._rigidbody.angularVelocity = Vector3.zero;
            this.transform.eulerAngles = Vector3.zero;
        }
    }
}
