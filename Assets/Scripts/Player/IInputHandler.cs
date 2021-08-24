using System;

public interface IInputHandler
{
    public event Action Jump;
    public event Action Rolling;

    public event Action RightButtonAction;
    public event Action RightButtonDownAction;
    public event Action LeftButtonAction;
    public event Action LeftButtonDownAction;
    
    public int MovementDirection { get; }
}