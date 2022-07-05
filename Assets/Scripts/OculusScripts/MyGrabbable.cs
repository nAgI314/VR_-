
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(Outline))]
public class MyGrabbable : MonoBehaviour
{
    public bool IsGrabbed
    {
        get => _isGrabbed;
    }

    private Rigidbody _rigidbody;
    private Transform _defaultParent;
    private Outline _outline;
    private bool _isGrabbed = false;

    public bool OutLineEnabled
    {
        get { return _outline.enabled; }
        set { _outline.enabled = value; }
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    void Start()
    {
        _defaultParent = transform.parent;
    }

    private IDisposable _grabDisposable;
    public void Grab(Transform handTrans)
    {
        if (_isGrabbed)
        {
            return;
        }

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
        }
        _isGrabbed = true;
        _grabDisposable?.Dispose();
        _grabDisposable = this.UpdateAsObservable()
            // ��܂Ō���Ȃ��߂��Ȃ�܂Ŏ������߂Â���
            .TakeWhile(_ => (transform.position - handTrans.position).sqrMagnitude > 0.1f * 0.1f).Subscribe(
                _ =>
                {
                    // ��܂ŃI�u�W�F�N�g���߂Â���
                    Vector3 dir = (handTrans.position - transform.position).normalized;
                    transform.position += dir * (10f * Time.deltaTime);
                }, () =>
                {
                    // �w�Ǌ�����
                    // ��܂Ō���Ȃ��߂��Ȃ����ꍇ�A�e����ɂ��Ċ���
                    transform.parent = handTrans;
                    transform.position = handTrans.position;
                }).AddTo(this);
    }

    public void Release(Vector3 controllerAcc)
    {
        if (!_isGrabbed)
        {
            return;
        }

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(controllerAcc, ForceMode.Impulse);
        }
        _grabDisposable?.Dispose();
        _isGrabbed = false;
        transform.parent = _defaultParent;
    }
}
    
