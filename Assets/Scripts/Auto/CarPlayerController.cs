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
            foreach(GameObject go in _backHeadlights)
                go.SetActive(true);
        else
            foreach (GameObject go in _backHeadlights)
                go.SetActive(false);
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
