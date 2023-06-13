using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CarPlayerInput))]

[System.Serializable]
public class AxleInfo
{
    public WheelCollider RightWheel;
    public WheelCollider LeftWheel;
    public Transform VisualRightWheel;
    public Transform VisualLeftWheel;
    public bool Steering;
    public bool Motor;
}

public class CarPlayerController : MonoBehaviour
{
    [SerializeField] private AxleInfo[] _carAxis;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _steerAngle = 26;
    [SerializeField] private GameObject[] _backHeadlights;
    [Header("Smoke settings")]
    [SerializeField] private ParticleSystem[] _smokePs;
    [SerializeField] private float _minSpeedForSmoke = 20;
    [SerializeField] private float _minAngleForSmoke = 30;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(float verInput, float horInput)
    {
        foreach(AxleInfo axle in _carAxis)
        {
            if(axle.Steering)
            {
                axle.LeftWheel.steerAngle = axle.RightWheel.steerAngle = _steerAngle * horInput;
            }
            if(axle.Motor)
            {
                axle.LeftWheel.motorTorque = axle.RightWheel.motorTorque = _moveSpeed * verInput;
            }
            VisualizeWheel(axle.RightWheel, axle.VisualRightWheel);
            VisualizeWheel(axle.LeftWheel, axle.VisualLeftWheel);
        }
        if(verInput < 0)
            SwitchBackLight(true);
        else
            SwitchBackLight(false);
        EmitSmokeFromWheels();
    }
    
    private void EmitSmokeFromWheels()
    {
        if(_rigidbody.velocity.magnitude > _minSpeedForSmoke)
        {
            float angle = Quaternion.Angle(Quaternion.LookRotation(_rigidbody.velocity, Vector3.up), Quaternion.LookRotation(transform.forward, Vector3.up));
            if(angle > _minAngleForSmoke && angle < 180 - _minAngleForSmoke)
                SwitchSmoke(true);
            else
                SwitchSmoke(false);
        }
        else
        {
            SwitchSmoke(false);
        }
    }
    private void SwitchSmoke(bool enable)
    {        
        foreach(ParticleSystem ps in _smokePs)
        {
            ParticleSystem.EmissionModule em = ps.emission;
            em.enabled = enable;
        }
    }

    private void SwitchBackLight(bool enable)
    {
        foreach (GameObject go in _backHeadlights)
            go.SetActive(enable);
    }

    private void VisualizeWheel(WheelCollider col, Transform visualWheel)
    {
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);
        visualWheel.position = position;
        visualWheel.rotation = rotation;
    }
}
