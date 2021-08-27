public interface IInputHandler
{
    public InputAction Jump { get; }
    public InputAction Rolling { get;  }
    public InputAction Fire { get;  }
    public InputAction ShieldButtonDownAction { get; }
    public InputAction DownButtonAction { get;  }
    public InputAction RightButtonAction { get; }
    public InputAction RightButtonDownAction { get; }
    public InputAction LeftButtonAction { get; }
    public InputAction LeftButtonDownAction { get; }

    public int MovementDirection { get; }
}