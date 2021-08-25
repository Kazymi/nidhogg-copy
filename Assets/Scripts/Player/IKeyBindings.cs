using UnityEngine;

public interface IKeyBindings
{
    KeyCode LeftButton { get; }
    KeyCode RightButton { get; }
    KeyCode JumpButton { get; }
    KeyCode DownButton { get; }
    KeyCode FireButton { get; }
}