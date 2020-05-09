using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoadGripped : GrippedBase
{
    [SerializeField] private float _releasingSpeed = 20.0f;
    [SerializeField] protected PoisonField _poisonField;
    [SerializeField] protected float _damageActiveSpeed = 2.0f;
    [SerializeField] protected Vector3 _adjustAngle = new Vector3(90, 0, 0);

    private void Awake()
    {
        this._poisonField.gameObject.SetActive(false);
    }

    private void Update()
    {
        this.HandleObject();
        this.DamageActive();
    }

    public override void Gripped(Grip grip)
    {
        base.Gripped(grip);
    }

    public override void Released(Grip grip)
    {
        base.Released(grip);
        this._rigidbody.velocity = transform.forward * this._releasingSpeed;
        StartCoroutine("DamageDelete");
    }

    protected override void HandleObject()
    {
        if (!this._grip)
            return;

        base.HandleObject();
        var angle = transform.eulerAngles + this._adjustAngle;
        transform.localEulerAngles = angle;
    }

    protected virtual void DamageActive()
    {
        Debug.Log(Vector3.Distance(Vector3.zero, this._rigidbody.velocity));
        this._poisonField.gameObject.SetActive
            (Vector3.Distance(Vector3.zero, this._rigidbody.velocity) > this._damageActiveSpeed);
    }
}
