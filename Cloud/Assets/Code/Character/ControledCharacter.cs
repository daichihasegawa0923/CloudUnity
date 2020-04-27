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

    [SerializeField] protected GameObject MainCamera { set; get; }
    [SerializeField] protected Vector3 _cameraDistance;

    private Vector3 _runAimPosition;

    private void Awake()
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this._cameraWork = GetComponent<CameraWork>();
        this._animator = GetComponent<Animator>();
        this.MainCamera = FindObjectOfType<Camera>().gameObject;
        this._respornPosition = transform.position;
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
        CameraChase();
    }

    protected void CameraChase()
    {
        if (this.MainCamera != null)
        {
            var distance = ((this.transform.position + this._cameraDistance) - MainCamera.transform.position);
            MainCamera.transform.position += distance * 0.01f;
            if (Vector3.Distance(Vector3.zero,distance) > 10)
            {
                MainCamera.transform.position = this.transform.position + this._cameraDistance;
            }
        }
    }

    private void LateUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        // ChangePerspectiveInMouseAction();
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
        // 回転の制限
        this._rigidbody.angularVelocity = Vector3.zero;

        //　クリック時イベント
        if (Input.GetMouseButtonDown(0))
        {
            this._runAimPosition = this.GetClickPosition(transform.position);
        }

        // 動き
        this.OperateMotion();
    }

    protected void OperateMotion()
    {
        if (Vector3.Distance(transform.position, this._runAimPosition) > 0.50f)
        {
            transform.LookAt(this._runAimPosition);
            var spin = transform.eulerAngles;
            spin.x = 0;
            spin.z = 0;
            transform.eulerAngles = spin;

            var speed = (this._runAimPosition - transform.position);
            if (Vector3.Distance(speed, Vector3.zero) > Vector3.Distance(transform.forward, Vector3.zero))
                speed = transform.forward;

            speed *= this._moveSpeed;

            speed.y = this._rigidbody.velocity.y;
            this._rigidbody.velocity = speed;

            if (!this._animator.GetBool("walk"))
                this._animator.SetBool("walk", true);

            if (this.IsStepIsFront())
            {
                var position = transform.forward;
                position.y += 1;
                transform.position += position;
            }
        }
        else
        {
            var motion = this._rigidbody.velocity;
            motion = Vector3.zero;
            motion.y = this._rigidbody.velocity.y;
            this._rigidbody.velocity = motion;
            if (this._animator.GetBool("walk"))
                this._animator.SetBool("walk", false);
        }
    }

    protected bool IsStepIsFront()
    {
        var position_stomach = transform.position + transform.forward * 0.5f;
        var position_head = position_stomach;
        position_head.y += 1.0f;

        var ray_stomach = new Ray(position_stomach, transform.forward);
        var ray_head = new Ray(position_head, transform.forward);

        
        var isHit_stomach = Physics.Raycast(ray_stomach,0.5f);
        var isHit_head = Physics.Raycast(ray_head, 0.5f);

        Debug.DrawRay(position_head, transform.forward,Color.blue);
        Debug.DrawRay(position_stomach, transform.forward, Color.blue);

        return isHit_stomach && !isHit_head;
    }

    protected Vector3 GetClickPosition(Vector3 currentPosition)
    {
        var ray = this.MainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) ? hit.point : currentPosition;
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
