using daichHasegawaUtil;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CameraWork))]
public class ControledCharacter : MonoBehaviourPun
{

    protected CharacterController _characterController;
    protected CameraWork _cameraWork;

    protected Animator _animator;
    protected Vector3 _noSpeed = new Vector3(0, 0, 0);
    [SerializeField]
    protected float _moveSpeed = 0.01f;

    protected virtual void Awake()
    {
        this._characterController = GetComponent<CharacterController>();
        this._cameraWork = GetComponent<CameraWork>();
        this._animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (photonView.IsMine)
        {
            //_cameraWork.OnStartFollowing();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        ControlByKey();
        ControlSpinByKey();
    }

    protected virtual void ControlByKey()
    {
        var code = InputUtil.GetInputtingKeyCode();
        var motion = this._characterController.velocity;
        if(code == KeyCode.W)
        {
            motion = transform.forward;
            _animator.SetBool("walk", true);
        }
        else
        {
            motion = this._noSpeed;
            _animator.SetBool("walk", false);
        }
        motion *= this._moveSpeed;
        _characterController.Move(motion);
    }

    protected virtual void ControlSpinByKey()
    {
        var code = InputUtil.GetInputtingKeyCode();
        var spin = transform.eulerAngles;
        if (code == KeyCode.D)
        {
            spin.y += 1f;
        }
        else if (code == KeyCode.A)
        {
            spin.y -= 1f;
        }
        transform.eulerAngles = spin;
    }
}
