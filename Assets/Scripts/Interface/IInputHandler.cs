public interface IInputHandler
{
    public InputAction Jump { get; }
    public InputAction Rolling { get;  }
    public InputAction Fire { get;  }
    public InputAction FastAttack { get;  }
    public InputAction EquipAction { get; }
    public InputAction SwapWeaponAction { get; }
    public InputAction ShieldButtonDownAction { get; }
    public InputAction DownButtonAction { get;  }
    public InputAction RightButtonAction { get; }
    public InputAction RightButtonDownAction { get; }
    public InputAction LeftButtonAction { get; }
    public InputAction LeftButtonDownAction { get; }
    public InputAction DropWeaponAction { get; }


    public int MovementDirection { get; }
}