using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction
{
    private event Action _action;
    
    public event Action Action
    {
        add => _action += value;
        remove => _action -= value;
    }

    public void Invoke()
    {
        _action?.Invoke();
    }
}
