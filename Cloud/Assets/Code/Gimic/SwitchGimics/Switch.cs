using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Material _pushMaterial;
    [SerializeField] Material _releasedMaterial;

    [SerializeField] GameObject _buttonPart;

    [SerializeField] private Vector3 _pushButtonPartPosition;
    [SerializeField] private Vector3 _releasedButtonPartPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsPushed())
        {
            _buttonPart.transform.localPosition = _pushButtonPartPosition;
            _buttonPart.GetComponent<MeshRenderer>().material = _pushMaterial;

        }
        else
        {
            _buttonPart.transform.localPosition = _releasedButtonPartPosition;
            _buttonPart.GetComponent<MeshRenderer>().material = _releasedMaterial;
        }
    }

    public virtual bool IsPushed()
    {
        Debug.DrawRay(transform.position, transform.up, Color.blue);
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit raycastHit;
        var isHit = Physics.Raycast(ray,out raycastHit,0.5f);
        if (!isHit)
            return false;

        if (raycastHit.collider.gameObject.GetComponent<Rigidbody>())
            return true;

        return false;
    }
}
