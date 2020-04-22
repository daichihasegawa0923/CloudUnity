using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resporner : MonoBehaviour
{
    public Vector3 RespornPosition { set; get; }
    [SerializeField] private float _respornDepth = -200.0f; 

    // Start is called before the first frame update
    void Start()
    {
        this.RespornPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _respornDepth)
        {
            transform.position = this.RespornPosition;
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
