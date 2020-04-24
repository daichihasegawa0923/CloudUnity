using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class GrippedBase : MonoBehaviour
{
    public abstract void Gripped(Grip grip);

    public abstract void Released(Grip grip);
}
