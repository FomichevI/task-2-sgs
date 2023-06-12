using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce =1.5f;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private float _fireAngle = -5;
    private bool _onGround;
    private Rigidbody _rigidbody;
    private HumanAnimator _humanAnimator;
    private float _lastVerInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _humanAnimator = GetComponent<HumanAnimator>();
    }

    public void Move(float horInput, float verInput)
    {
        if (_onGround)
        {
            if (verInput > 0)
                transform.Translate(Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
            else if (verInput < 0)
                transform.Translate(-Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
            if (Mathf.Abs(horInput) > 0.2)
                transform.Rotate(Vector3.up * horInput, _rotationSpeed * Time.fixedDeltaTime);
            if (_onGround)
                _humanAnimator.Move(verInput);
            _lastVerInput = verInput;
        }
        else
        {
            if (_lastVerInput > 0)
                transform.Translate(Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
            else if (_lastVerInput < 0)
                transform.Translate(-Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _onGround = true;
        }
    }

    public void Jump()
    {
        if (_onGround && _lastVerInput >= 0)
        {
            _rigidbody.AddForce(Vector3.up * 10000 * _jumpForce);
            _humanAnimator.Jump();
            _onGround = false;
        }
    }

    public void Fire()
    {
        if(_onGround)
        {
            _humanAnimator.StartFire();
            Quaternion newRotation = transform.rotation;
            newRotation.x = _fireAngle * Mathf.Deg2Rad;
            GameObject projectile = Instantiate(_arrowPrefab,_humanAnimator._lookTargetObject.transform.position, newRotation);
        }
    }
}
