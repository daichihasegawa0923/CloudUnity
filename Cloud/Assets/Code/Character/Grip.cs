using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour
{
    [SerializeField] private bool isGrip;
    [SerializeField] protected GameObject _grippedObject;
    [SerializeField] private ControledCharacter character;

    public ControledCharacter Character { get => character; protected set => character = value; }
    public bool IsGrip { get => isGrip; private set => isGrip = value; }

    private void Update()
    {
    }

    public void Gripping()
    {
        if (IsGrip || !_grippedObject || _grippedObject == this.Character.gameObject)
            return;

        IsGrip = true;
        if (_grippedObject.GetComponent<GrippedBase>())
            _grippedObject.GetComponent<GrippedBase>().Gripped(this);
    }

    public void Releasing()
    {
        IsGrip = false;

        if (_grippedObject != null && _grippedObject.GetComponent<GrippedBase>())
            _grippedObject.GetComponent<GrippedBase>().Released(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsGrip)
            return;
        if(other.gameObject.GetComponent<Rigidbody>())
            _grippedObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_grippedObject && !IsGrip)
            _grippedObject = null;
    }
}
