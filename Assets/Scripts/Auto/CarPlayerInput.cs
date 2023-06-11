using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CarPlayerController))]

public class CarPlayerInput : MonoBehaviour
{
    public static CarPlayerInput Instance;
    [SerializeField] private Button _forwardButton;

    private float _horizontalInput;
    private float _verticalInput;
    private CarPlayerController _carPlayerController;

    private bool _isForwardButtonPressed = false;
    private bool _isBackButtonPressed = false;
    private bool _isLeftButtonPressed = false;
    private bool _isRightButtonPressed = false;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        _carPlayerController = GetComponent<CarPlayerController>();
    }

    void FixedUpdate()
    {
        //if (Application.platform == RuntimePlatform.WindowsPlayer)
        //{
        //    _horizontalInput = Input.GetAxis("Horizontal");
        //    _verticalInput = Input.GetAxis("Vertical");
        //}

        if (_isForwardButtonPressed && _verticalInput < 1) _verticalInput += 0.1f;
        if (_isBackButtonPressed && _verticalInput > -1) _verticalInput -= 0.1f;
        if (!_isForwardButtonPressed && !_isBackButtonPressed && _verticalInput != 0)
            _verticalInput = _verticalInput < 0 ? (_verticalInput += 0.1f) : (_verticalInput -= 0.1f);
        if(Mathf.Abs(_verticalInput) < 0.1f) _verticalInput = 0;

        if (_isRightButtonPressed && _horizontalInput < 1) _horizontalInput += 0.1f;
        if (_isLeftButtonPressed && _horizontalInput > -1) _horizontalInput -= 0.1f;
        if (!_isRightButtonPressed && !_isLeftButtonPressed && _horizontalInput != 0)
            _horizontalInput = _horizontalInput < 0 ? (_horizontalInput += 0.1f) : (_horizontalInput -= 0.1f);
        if (Mathf.Abs(_horizontalInput) < 0.1f) _horizontalInput = 0;

        _carPlayerController.Move(_verticalInput, _horizontalInput);
    }

    public void PressForwardButton() { _isForwardButtonPressed = true; }
    public void UpForwardButton() { _isForwardButtonPressed = false; }
    public void PressBackButton() { _isBackButtonPressed = true; }
    public void UpBackButton() { _isBackButtonPressed = false; }
    public void PressLeftButton() { _isLeftButtonPressed = true; }
    public void UpLeftButton() { _isLeftButtonPressed = false; }
    public void PressRightButton() { _isRightButtonPressed = true; }
    public void UpRightButton() { _isRightButtonPressed = false; }

}
