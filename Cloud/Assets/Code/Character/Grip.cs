using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour
{
    [SerializeField] protected bool _isGrip;
    [SerializeField] protected GameObject _grippedObject;
    [SerializeField] private HingeJoint hJoint;
    [SerializeField] private ControledCharacter character;

    public ControledCharacter Character { get => character; protected set => character = value; }
    public HingeJoint HJoint { get => hJoint; protected set => hJoint = value; }

    private void Update()
    {
    }

    public void Gripping()
    {
        if (_isGrip || !_grippedObject || _grippedObject == this.Character.gameObject)
            return;

        _isGrip = true;
        this.HJoint = _grippedObject.AddComponent<HingeJoint>();
        this.HJoint.connectedBody = GetComponent<Rigidbody>();
        this.HJoint.useLimits = true;
        this.HJoint.enableCollision = true;

        if (_grippedObject.GetComponent<GrippedBase>())
            _grippedObject.GetComponent<GrippedBase>().Gripped(this);
    }

    public void Releasing()
    {
        _isGrip = false;
        Destroy(this.HJoint);

        if (_grippedObject != null && _grippedObject.GetComponent<GrippedBase>())
            _grippedObject.GetComponent<GrippedBase>().Released(this);
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
        if (_grippedObject && !_isGrip)
            _grippedObject = null;
    }
}
