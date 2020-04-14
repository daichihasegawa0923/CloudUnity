using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 firstPosition;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected AudioSource _audioSource;

    public Vector3 FirstPosition { get => firstPosition; set => firstPosition = value; }

    // Start is called before the first frame update
    void Start()
    {
        this.FirstPosition = this.transform.position;
        this._rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FixPosition();
    }

    public void ResetPosition()
    {
        transform.position = this.FirstPosition;
    }

    public void Kicked()
    {
        this._audioSource.Play();
    }

    public void FixPosition()
    {
        if (Vector3.Distance(transform.position,FirstPosition) > 200)
        {
            this._rigidbody.velocity = Vector3.zero;
            this.ResetPosition();
        }
    }
}
