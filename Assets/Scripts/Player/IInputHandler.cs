using System;

public interface IInputHandler
{
    public Action Jump { get; set; }
    public Action Rolling { get; set; }

    public event Action RightButtonAction;
    public event Action RightButtonDownAction;
    public event Action LeftButtonAction;
    public event Action LeftButtonDownAction;

    public int MovementDirection { get; }
}