using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchGimic : MonoBehaviour
{
    public virtual Switch Switch { private set { _switch = value; } get { return _switch; } }
    [SerializeField] private Switch _switch;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this.Switch.IsPushed())
            PushAction();
        else
            ReleaseAction();
    }

    public abstract void PushAction();

    public abstract void ReleaseAction();
}
