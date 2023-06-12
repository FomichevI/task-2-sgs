using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimator : MonoBehaviour
{
    public Transform _lookTargetObject;
    [SerializeField] private Transform _leftHandObject;
    private Animator _animator;
    private int _aimLayerIndex;
    private bool _isAimAnimation = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aimLayerIndex = _animator.GetLayerIndex("Aiming");
    }

    public void EndShowAim()
    {
        _isAimAnimation = false;
    }

    public void StartFire()
    {
        _animator.SetTrigger("Fire");
        _isAimAnimation = true;
    }

    private void FixedUpdate()
    {
        if (_isAimAnimation && _animator.GetLayerWeight(_aimLayerIndex) < 1)
            _animator.SetLayerWeight(_aimLayerIndex, _animator.GetLayerWeight(_aimLayerIndex) + 0.1f);
        else if (!_isAimAnimation && _animator.GetLayerWeight(_aimLayerIndex) > 0)
            _animator.SetLayerWeight(_aimLayerIndex, _animator.GetLayerWeight(_aimLayerIndex) - 0.1f);
    }

    public void Move(float verInput)
    {
        _animator.SetFloat("Speed", verInput);
    }
    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    private void OnAnimatorIK()
    {
        if (_lookTargetObject != null)
        {
            _animator.SetLookAtWeight(_animator.GetLayerWeight(_aimLayerIndex));
            _animator.SetLookAtPosition(_lookTargetObject.position);
        }
        if (_leftHandObject != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _animator.GetLayerWeight(_aimLayerIndex));
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _animator.GetLayerWeight(_aimLayerIndex));
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _lookTargetObject.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _lookTargetObject.rotation);
        }

    }

}
