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

    [SerializeField] protected float _jumpPower = 3.0f;

    [SerializeField] protected Grip _grip;

    [SerializeField] public Vector3 _respornPosition;

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

        _respornPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        ControlByKey();
        Jump();
        RespornByFall();
    }

    private void LateUpdate()
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
        if (neckSpin.x > 180 && neckSpin.x < 344)
            neckSpin.x = 344;
        else if (neckSpin.x < 180 && neckSpin.x > 86)
            neckSpin.x = 85;
        spin.y += mouseSpinX;
        spin.z = 0;
        transform.eulerAngles = spin;
        this._neck.transform.localEulerAngles = neckSpin;
    }

    protected void ControlByKey()
    {
        var motion = this._rigidbody.velocity;
        if(Input.GetKey(KeyCode.W))
        {
            motion = transform.forward;
            motion *= this._moveSpeed;
            _animator.SetBool("walk", true);
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            motion = this._noSpeed;
            _animator.SetBool("walk", false);
        }
        motion.y = this._rigidbody.velocity.y;
        this._rigidbody.velocity  = motion;
        this._rigidbody.angularVelocity = Vector3.zero;

        if(Input.GetKey(KeyCode.A))
        {
            _animator.SetBool("grip", true);
            _grip.Gripping();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetBool("grip", false);
            _grip.Releasing();
        }
    }

    protected void RespornByFall()
    {
        if (transform.position.y < -200)
        {
            this._rigidbody.velocity = Vector3.zero;
            this.transform.position = this._respornPosition;
        }
    }

    protected void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var allRayHit = Physics.RaycastAll(transform.position+transform.up, -transform.up,1.0f);
            Debug.DrawRay(transform.position, -transform.up,Color.blue);
            foreach(var hit in allRayHit)
            {
                if (!hit.collider.gameObject.Equals(gameObject))
                    this._rigidbody.velocity += transform.up * this._jumpPower;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
