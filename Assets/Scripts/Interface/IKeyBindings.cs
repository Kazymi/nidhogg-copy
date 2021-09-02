using UnityEngine;

public interface IKeyBindings
{
    KeyCode LeftButton { get; }
    KeyCode EquipButton { get; }
    KeyCode RightButton { get; }
    KeyCode JumpButton { get; }
    KeyCode DownButton { get; }
    KeyCode FireButton { get; }
    KeyCode ShieldButton { get; }
    KeyCode DropWeaponButton { get; } 
    KeyCode SwapWeaponButton { get; } 
    float ClickThreshold { get; }
}