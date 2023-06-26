using UnityEngine;

[RequireComponent(typeof(HumanPlayerInput))]
[RequireComponent(typeof(HumanAnimator))]

public class HumanPlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce =1.5f;
    [SerializeField] private float _fireAngle = -5;
    [SerializeField] private float _fireColldown = 1;
    private bool _onGround;
    private Rigidbody _rigidbody;
    private HumanAnimator _humanAnimator;
    //Необходимо для определения направления во время движения в воздухе
    private float _lastVerInput; 
    private float _currentFireCd;
    private const float _jumpForceMultiplier = 10000f;
    private const int _groundLayerIndex = 6;

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
        if (collision.gameObject.layer == _groundLayerIndex)
        {
            _onGround = true;
        }
    }

    public void Jump()
    {
        if (_onGround && _lastVerInput >= 0)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForceMultiplier * _jumpForce);
            _humanAnimator.Jump();
            _onGround = false;
        }
    }

    public void Fire()
    {
        if(_onGround && _currentFireCd <= 0)
        {
            _humanAnimator.StartFire();
            Quaternion newRotation = transform.rotation;
            newRotation.x = _fireAngle * Mathf.Deg2Rad;
            GameObject projectile = Instantiate(Resources.Load<GameObject>("Prefabs/Arrow"),_humanAnimator._lookTargetObject.transform.position, newRotation);
            _currentFireCd = _fireColldown;
        }
    }

    private void FixedUpdate()
    {
        if (_currentFireCd > 0)
            _currentFireCd -= Time.fixedDeltaTime;
    }
}
