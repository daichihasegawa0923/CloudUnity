using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour
{
    [SerializeField] protected bool _isGrip;
    [SerializeField] protected GameObject _grippedObject;
    [SerializeField] protected HingeJoint _hingeJoint;


    private void Update()
    {
        if (this._hingeJoint)
            Debug.Log(this._hingeJoint.currentForce);
    }

    public void Gripping()
    {
        if (_isGrip || !_grippedObject)
            return;

        _isGrip = true;
        this._hingeJoint = _grippedObject.AddComponent<HingeJoint>();
        this._hingeJoint.connectedBody = GetComponent<Rigidbody>();
        this._hingeJoint.useLimits = true;
        this._hingeJoint.enableCollision = true;
        this._hingeJoint.breakForce = 1000.0f;
    }

    public void Releasing()
    {
        _isGrip = false;
        Destroy(this._hingeJoint);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isGrip)
            return;
        if(other.gameObject.GetComponent<Rigidbody>())
            _grippedObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_grippedObject)
            _grippedObject = null;
    }
}
