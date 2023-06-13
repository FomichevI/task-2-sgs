using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarPlayerInput : MonoBehaviour
{
    [SerializeField] private Button _forwardButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _leftButton;

    private float _horizontalInput;
    private float _verticalInput;
    private CarPlayerController _carPlayerController;

    private bool _isForwardButtonPressed = false;
    private bool _isBackButtonPressed = false;
    private bool _isLeftButtonPressed = false;
    private bool _isRightButtonPressed = false;

    void Start()
    {
        _carPlayerController = GetComponent<CarPlayerController>();
        AddEventToButton(_forwardButton, PressForwardButton, UpForwardButton);
        AddEventToButton(_backButton, PressBackButton, UpBackButton);
        AddEventToButton(_rightButton, PressRightButton, UpRightButton);
        AddEventToButton(_leftButton, PressLeftButton, UpLeftButton);
    }

    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        if (_isForwardButtonPressed && _verticalInput < 1) _verticalInput += 0.1f;
        if (_isBackButtonPressed && _verticalInput > -1) _verticalInput -= 0.1f;
        if (!_isForwardButtonPressed && !_isBackButtonPressed && _verticalInput != 0)
            _verticalInput = _verticalInput < 0 ? (_verticalInput += 0.1f) : (_verticalInput -= 0.1f);
        if (Mathf.Abs(_verticalInput) < 0.1f) _verticalInput = 0;

        if (_isRightButtonPressed && _horizontalInput < 1) _horizontalInput += 0.1f;
        if (_isLeftButtonPressed && _horizontalInput > -1) _horizontalInput -= 0.1f;
        if (!_isRightButtonPressed && !_isLeftButtonPressed && _horizontalInput != 0)
            _horizontalInput = _horizontalInput < 0 ? (_horizontalInput += 0.1f) : (_horizontalInput -= 0.1f);
        if (Mathf.Abs(_horizontalInput) < 0.1f) _horizontalInput = 0;

        _carPlayerController.Move(_verticalInput, _horizontalInput);
    }

    private void AddEventToButton(Button button, Action actionPoinerDown, Action actionPoinerUp) //метод добавляет 2 события к кнопке (при удержании и при отпускании кнопки)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry1.callback.AddListener((data) => actionPoinerDown());
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerUp;
        entry2.callback.AddListener((data) => actionPoinerUp());
        trigger.triggers.Add(entry1);
        trigger.triggers.Add(entry2);
    }
    private void AddEventToButton(Button button, Action action, EventTriggerType triggerType) //метод для будущего улучшения скрипта
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => action());
        trigger.triggers.Add(entry);
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
