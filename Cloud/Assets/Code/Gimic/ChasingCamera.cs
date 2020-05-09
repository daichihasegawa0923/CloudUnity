using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraDistance;
    public ControledCharacter ControledCharacter { get; set; }

    private void Update()
    {
        if (this.ControledCharacter)
        {
            var distance = ((this.ControledCharacter.transform.position + this._cameraDistance) - transform.position);
            transform.position += distance * 0.01f;
        }
    }
}
