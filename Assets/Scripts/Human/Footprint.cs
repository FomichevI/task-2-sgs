using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _hidingTime;
    private float _currentHideTime;
    //Модуль, на который будет уменьшаться размер объекта со временем в каждом кадре
    private Vector3 _moduleScale; 

    private void Start()
    {
        _currentHideTime = _hidingTime * 1f;
        _moduleScale = transform.localScale * -Time.fixedDeltaTime / _hidingTime;
    }

    private void FixedUpdate()
    {
        if (_lifeTime > 0)
        {
            _lifeTime -= Time.fixedDeltaTime;
        }
        else
        {
            _currentHideTime -= Time.fixedDeltaTime;
            if (_currentHideTime <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localScale += _moduleScale;
            }
        }
    }
}
