using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction
{

    public event Action Action;

    public void Invoke()
    {
        Action?.Invoke();
    }
}
