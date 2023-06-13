using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _hidingTime;
    private float _currentHideTime;
    private Vector3 _moduleScale;

    private void Start()
    {
        _currentHideTime = _hidingTime * 1f;
        _moduleScale = transform.localScale * -0.02f / _hidingTime;
    }

    private void FixedUpdate()
    {
        if (_lifeTime > 0)
        {
            _lifeTime -= Time.deltaTime;
        }
        else
        {
            _currentHideTime -= Time.deltaTime;
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
