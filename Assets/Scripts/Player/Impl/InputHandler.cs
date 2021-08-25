using System;
using UnityEngine;
using Zenject;

public class InputHandler : MonoBehaviour, IInputHandler
{
    private const float _click_threshld = 0.25f;
    private float _timeСlick = 0;
    private KeyCode _lastKey;
    private IKeyBindings _keyBindings;
    
    public InputAction Jump { get; set; } = new InputAction();
    public InputAction Rolling { get; set; } = new InputAction();
    public InputAction Fire { get; set; } = new InputAction();
    public InputAction DownButtonAction { get; set; } = new InputAction();
    public InputAction RightButtonAction { get; set; } = new InputAction();
    public InputAction RightButtonDownAction { get; set; } = new InputAction();
    public InputAction LeftButtonAction { get; set; } = new InputAction();
    public InputAction LeftButtonDownAction { get; set; } = new InputAction();
    public int MovementDirection { get; private set; }

    [Inject]
    private void Construct(IKeyBindings keyBindings)
    {
        _keyBindings = keyBindings;
    }

    private void Start()
    {
        RightButtonAction.Action += () => MovementDirection = 1;
        LeftButtonAction.Action += () => MovementDirection = -1;

        RightButtonDownAction.Action += () =>
        {
            var key = _keyBindings.RightButton;
            DoubleClickCheck(key);
            _lastKey = key;
        };

        LeftButtonDownAction.Action += () =>
        {
            var key = _keyBindings.LeftButton;
            DoubleClickCheck(key);
            _lastKey = key;
        };
    }

    private void Update()
    {
        MovementDirection = 0;
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if ((Input.GetKey(_keyBindings.RightButton) && Input.GetKey(_keyBindings.LeftButton)) == false)
        {
            if (Input.GetKey(_keyBindings.RightButton))
            {
                RightButtonAction.Invoke();
            }
            if (Input.GetKey(_keyBindings.LeftButton))
            {
                LeftButtonAction.Invoke();
            }
        }

        if (Input.GetKeyDown(_keyBindings.RightButton))
        {
            RightButtonDownAction.Invoke();
        }

        if (Input.GetKeyDown(_keyBindings.LeftButton))
        {
            LeftButtonDownAction.Invoke();
        }

        if (Input.GetKeyDown(_keyBindings.DownButton))
        {
            DownButtonAction.Invoke();
        }

        if (Input.GetKey(_keyBindings.FireButton))
        {
            Fire.Invoke();
        }
    }

    private void DoubleClickCheck(KeyCode currentKey)
    {
        if (currentKey != _lastKey)
        {
            return;
        }

        if (Time.time - _timeСlick < _click_threshld)
        {
            Rolling.Invoke();
        }

        _timeСlick = Time.time;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_keyBindings.JumpButton))
        {
            Jump?.Invoke();
        }
    }
}