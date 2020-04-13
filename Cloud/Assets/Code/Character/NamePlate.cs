using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePlate : MonoBehaviour
{
    [SerializeField] protected Camera _camera;

    [SerializeField] private Text nameText;

    public Text NameText { get => nameText; set => nameText = value; }

    // Start is called before the first frame update

    protected void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        FindCamera();
    }

    protected void FindCamera()
    {
        if(!_camera)
        {
            _camera = FindObjectOfType<Camera>();
            return;
        }
        transform.LookAt(_camera.gameObject.transform.position);
    }
}
