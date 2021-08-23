using System;
using UnityEngine;
using Zenject;

public class InputHandler : MonoBehaviour, IInputHandler
{
    public event Action Jump;

    private IKeyBindings _keyBindings;

    public int MovementDirection { get; private set; }

    [Inject]
    private void Construct(IKeyBindings keyBindings)
    {
        _keyBindings = keyBindings;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(_keyBindings.RightButton))
        {
            MovementDirection = 1;
        }
        else if (Input.GetKey(_keyBindings.LeftButton))
        {
            MovementDirection = -1;
        }
        else
        {
            MovementDirection = 0;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_keyBindings.JumpButton))
        {
            Jump?.Invoke();
        }
    }
}