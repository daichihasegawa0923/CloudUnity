using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGrippedObject : GrippedBase
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private ControledCharacter _character;

    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_character != null)
        {
            _character.transform.position = transform.position - _position;
            var speed = _rigidbody.velocity;
            speed.x += _character.gameObject.GetComponent<Rigidbody>().velocity.x * 0.1f;
            speed.z += _character.gameObject.GetComponent<Rigidbody>().velocity.z * 0.1f;
            _rigidbody.velocity = speed;
        }
    }

    public override void Gripped(Grip grip)
    {
        _character = grip.Character;
        Destroy(grip.HJoint);
        _position = transform.position - _character.transform.position;
    }

    public override void Released(Grip grip)
    {
        var speed = _character.gameObject.GetComponent<Rigidbody>().velocity;
        speed.x += _rigidbody.velocity.x;
        speed.z += _rigidbody.velocity.z;
        speed.y = _rigidbody.velocity.y;
        _character.gameObject.GetComponent<Rigidbody>().velocity = speed;

        _character = null;
    }
}
