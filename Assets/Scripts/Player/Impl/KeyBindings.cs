using UnityEngine;

[CreateAssetMenu(menuName = "Key Bindings", fileName = "KeyBindings")]
public class KeyBindings : ScriptableObject, IKeyBindings
{
    [SerializeField] private KeyCode leftButton;
    [SerializeField] private KeyCode rightButton;
    [SerializeField] private KeyCode jumpButton;

    public KeyCode LeftButton => leftButton;
    public KeyCode RightButton => rightButton;
    public KeyCode JumpButton => jumpButton;
}