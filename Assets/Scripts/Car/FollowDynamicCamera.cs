using UnityEngine;

public class FollowDynamicCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _rotationSpeed;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _playerTrans.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _playerTrans.position + transform.rotation * _offset,
           _followSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _playerTrans.rotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}
