using UnityEngine;
using Zenject;

public class InputHandler : MonoBehaviour, IInputHandler
{
    private float _clickDelta = 0;
    private KeyCode _lastKey;
    private IKeyBindings _keyBindings;

    public InputAction Jump { get; } = new InputAction();
    public InputAction Rolling { get; } = new InputAction();
    public InputAction ShieldButtonDownAction { get; } = new InputAction();
    public InputAction DropWeaponAction { get; } = new InputAction();
    public InputAction Fire { get; } = new InputAction();
    public InputAction DownButtonAction { get; } = new InputAction();
    public InputAction RightButtonAction { get; } = new InputAction();
    public InputAction RightButtonDownAction { get; } = new InputAction();
    public InputAction LeftButtonAction { get; } = new InputAction();
    public InputAction LeftButtonDownAction { get; } = new InputAction();
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

        RightButtonDownAction.Action += () => DoubleClickCheck(_keyBindings.RightButton);
        LeftButtonDownAction.Action += () => DoubleClickCheck(_keyBindings.LeftButton);
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

        if (Input.GetKeyDown(_keyBindings.ShieldButton))
        {
            ShieldButtonDownAction.Invoke();
        }

        if (Input.GetKeyDown(_keyBindings.DropWeaponButton))
        {
            DropWeaponAction.Invoke();
        }
    }

    private void DoubleClickCheck(KeyCode currentKey)
    {
        if (currentKey != _lastKey)
        {
            _lastKey = currentKey;
            return;
        }

        if (Time.time - _clickDelta < _keyBindings.ClickThreshold)
        {
            Rolling.Invoke();
        }

        _clickDelta = Time.time;
        _lastKey = currentKey;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_keyBindings.JumpButton))
        {
            Jump?.Invoke();
        }
    }
}