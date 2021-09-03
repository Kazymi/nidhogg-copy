using System;

public class InputAction
{
    public event Action Action;

    public void Invoke()
    {
        Action?.Invoke();
    }
}