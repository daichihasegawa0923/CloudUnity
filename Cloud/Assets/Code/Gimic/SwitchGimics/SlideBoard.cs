using UnityEngine;

public class SlideBoard : SwitchGimic
{
    [SerializeField] protected Vector3 _startPosition;
    [SerializeField] protected Vector3 _endPosition;
    [SerializeField] private float _speed = 0.1f;

    public override void PushAction()
    {
        transform.position += (_endPosition - transform.position) * this._speed;
    }

    public override void ReleaseAction()
    {
        transform.position += (_startPosition - transform.position) * this._speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }
}
