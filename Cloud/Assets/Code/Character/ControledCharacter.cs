using daichHasegawaUtil;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class ControledCharacter : MonoBehaviourPun
{

    protected Rigidbody _rigidbody;
    protected CameraWork _cameraWork;

    [SerializeField] protected GameObject[] _arms;
    [SerializeField] protected Vector3[] _armEulerAngle;
    [SerializeField] protected GameObject _neck;

    [SerializeField] protected float _sensitivity = 4.0f; 

    protected Animator _animator;
    protected Vector3 _noSpeed = new Vector3(0, 0, 0);
    [SerializeField]
    protected float _moveSpeed = 0.01f;

    [SerializeField] protected Grip _grip;

    private void Awake()
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this._cameraWork = GetComponent<CameraWork>();
        this._animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            _cameraWork.OnStartFollowing();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        ControlByKey();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        ChangePerspectiveInMouseAction();
    }

    protected void ChangePerspectiveInMouseAction()
    {
        var mouseSpinX = Input.GetAxis("Mouse X") * this._sensitivity;
        var mouseSpinY = Input.GetAxis("Mouse Y") * this._sensitivity;

        var spin = transform.eulerAngles;
        var neckSpin = this._neck.transform.localEulerAngles;
        neckSpin.x -= mouseSpinY;
        if (neckSpin.x > 180 && neckSpin.x < 324)
            neckSpin.x = 325;
        else if (spin.x < 180 && spin.x > 56)
            spin.x = 55;
        spin.y += mouseSpinX;
        spin.z = 0;
        transform.eulerAngles = spin;
        this._neck.transform.localEulerAngles = neckSpin;
    }

    protected void ControlByKey()
    {
        var code = InputUtil.GetInputtingKeyCode();
        var motion = this._rigidbody.velocity;
        if(code == KeyCode.W)
        {
            motion = transform.forward;
            motion.y = this._rigidbody.velocity.y;
            _animator.SetBool("walk", true);
        }
        else
        {
            motion = this._noSpeed;
            motion.y = this._rigidbody.velocity.y;
            _animator.SetBool("walk", false);
        }
        motion *= this._moveSpeed;
        this._rigidbody.velocity  = motion;
        this._rigidbody.angularVelocity = Vector3.zero;

        if(code == KeyCode.A)
        {
            _animator.SetBool("grip", true);
            _grip.Gripping();
        }
        else
        {
            _animator.SetBool("grip", false);
            _grip.Releasing();
        }
    }
}
