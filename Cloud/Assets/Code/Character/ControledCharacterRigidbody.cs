using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using daichHasegawaUtil;

[RequireComponent(typeof(Rigidbody))]
public class ControledCharacterRigidbody : ControledCharacter
{
    protected Rigidbody _rigidbody;
    [SerializeField] protected float _speed;
    protected override void Awake()
    {
        base.Awake();
        this._rigidbody = GetComponent<Rigidbody>();
        
    }
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void ControlByKey()
    {
        var key = InputUtil.GetInputtingKeyCode();
        var speed = this._rigidbody.velocity;
        var spin = this.transform.eulerAngles;
        var isAnimation = true;
        switch (key) {
            case KeyCode.A:
                spin.y = -90.0f;
                speed.x = -this._speed;
                speed.z = 0;
                break;
            case KeyCode.D:
                spin.y = 90.0f;
                speed.x = this._speed;
                speed.z = 0;
                break;
            case KeyCode.S:
                spin.y = 180.0f;
                speed.x = 0;
                speed.z = -this._speed;
                break;
            case KeyCode.W:
                spin.y = 0;
                speed.z = this._speed;
                speed.x = 0;
                break;
            default:
                speed.z = 0;
                speed.x = 0;
                isAnimation = false;
                break;
        }
        this._animator.SetBool("walk",isAnimation);
        this._rigidbody.velocity = speed;
        this.transform.eulerAngles = spin;

    }
}
