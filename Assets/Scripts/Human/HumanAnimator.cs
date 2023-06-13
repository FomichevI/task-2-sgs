using UnityEngine;

public class HumanAnimator : MonoBehaviour
{
    public Transform _lookTargetObject; //����� ������ ����� ����������, � ��� ����� �������� ���� �� ����� ��������
    [SerializeField] private Transform _leftHandObject;
    private Animator _animator;
    private int _aimAnimatorLayerIndex;
    private bool _isAimAnimation = false; //��������� ��� ��������� �������� �������� ����� ������

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aimAnimatorLayerIndex = _animator.GetLayerIndex("Aiming");
    }

    public void EndShowAim() //���������� � ��������
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
        //�������� ������� ����� ������ ��������
        if (_isAimAnimation && _animator.GetLayerWeight(_aimAnimatorLayerIndex) < 1)
            _animator.SetLayerWeight(_aimAnimatorLayerIndex, _animator.GetLayerWeight(_aimAnimatorLayerIndex) + 0.2f);
        else if (!_isAimAnimation && _animator.GetLayerWeight(_aimAnimatorLayerIndex) > 0)
            _animator.SetLayerWeight(_aimAnimatorLayerIndex, _animator.GetLayerWeight(_aimAnimatorLayerIndex) - 0.2f);
    }

    public void Move(float verInput)
    {
        _animator.SetFloat("Speed", verInput);
        _animator.SetBool("OnTheGround", true);
    }
    public void Jump()
    {
        _animator.SetTrigger("Jump");
        _animator.SetBool("OnTheGround", false);
    }

    private void OnAnimatorIK() //����������� ����� ���� ���������, ����� ���  �������� ������
    {
        if (_lookTargetObject != null)
        {
            _animator.SetLookAtWeight(_animator.GetLayerWeight(_aimAnimatorLayerIndex));
            _animator.SetLookAtPosition(_lookTargetObject.position);
        }
        if (_leftHandObject != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _animator.GetLayerWeight(_aimAnimatorLayerIndex));
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _animator.GetLayerWeight(_aimAnimatorLayerIndex));
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _lookTargetObject.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _lookTargetObject.rotation);
        }
    }
}
