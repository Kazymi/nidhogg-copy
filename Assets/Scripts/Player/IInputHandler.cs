using System;

public interface IInputHandler
{
    public InputAction Jump { get; set; }
    public InputAction Rolling { get; set; }

    public InputAction Fire { get; set; }
    public InputAction DownButtonAction { get; set; }
    public InputAction RightButtonAction { get; set; }
    public InputAction RightButtonDownAction { get; set; }
    public InputAction LeftButtonAction { get; set; }
    public InputAction LeftButtonDownAction { get; set; }

    public int MovementDirection { get; }
}