using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInput : MonoBehaviour
{
    [SerializeField] private ClickedButton[] _buttons;

    private bool _isForwardButtonPressed = false;
    private bool _isBackButtonPressed = false;
    private bool _isLeftButtonPressed = false;
    private bool _isRightButtonPressed = false;

    private void Start()
    {
        foreach (ClickedButton cb in _buttons)
        {
            if (!cb.TargetButton.gameObject.GetComponent<EventTrigger>())
                cb.TargetButton.gameObject.AddComponent<EventTrigger>();
            cb.AddEvents();
        }
    }

    public void PressForwardButton() { _isForwardButtonPressed = true; Debug.Log("ForwardButtonPressed"); }
    public void UpForwardButton() { _isForwardButtonPressed = false; Debug.Log("ForwardButtonUp"); }
    public void PressBackButton() { _isBackButtonPressed = true; Debug.Log("BackButtonPressed"); }
    public void UpBackButton() { _isBackButtonPressed = false; Debug.Log("BackButtonUp"); }
    public void PressLeftButton() { _isLeftButtonPressed = true; Debug.Log("LeftButtonPressed"); }
    public void UpLeftButton() { _isLeftButtonPressed = false; Debug.Log("LeftButtonUp"); }
    public void PressRightButton() { _isRightButtonPressed = true; Debug.Log("RightButtonPressed"); }
    public void UpRightButton() { _isRightButtonPressed = false; Debug.Log("RightButtonUp"); }
}

[Serializable]
public class ClickedButton
{
    public Button TargetButton;
    public EventInfo[] ListInfo;

    public void AddEvents()
    {
        foreach (EventInfo eventInfo in ListInfo)
        {
            EventTrigger trigger = TargetButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            switch(eventInfo.Status)
            {
                case ButtonStatus.Click:
                    entry.eventID = EventTriggerType.PointerClick;
                    break;
                case ButtonStatus.Hold:
                    entry.eventID = EventTriggerType.PointerDown;
                    break;
                case ButtonStatus.Up:
                    entry.eventID = EventTriggerType.PointerUp;
                    break;
            }
            entry.callback.AddListener((data) => eventInfo.Event.Invoke());
            trigger.triggers.Add(entry);
        }
    }
}

public enum ButtonStatus {Click, Hold, Up}

[System.Serializable]
public class EventInfo
{
    public ButtonStatus Status;
    public UnityEvent Event;
}
