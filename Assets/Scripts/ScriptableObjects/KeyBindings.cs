﻿using UnityEngine;

[CreateAssetMenu(menuName = "Key Bindings", fileName = "KeyBindings")]
public class KeyBindings : ScriptableObject, IKeyBindings
{
    [SerializeField] private KeyCode leftButton;
    [SerializeField] private KeyCode rightButton;
    [SerializeField] private KeyCode jumpButton;
    [SerializeField] private KeyCode downButton;
    [SerializeField] private KeyCode fireButton;
    [SerializeField] private KeyCode shieldButton;
    [SerializeField] private KeyCode dropWeaponButton;
    [SerializeField] private float clickThreshold;
    public KeyCode LeftButton => leftButton;
    public KeyCode RightButton => rightButton;
    public KeyCode JumpButton => jumpButton;
    public KeyCode DownButton => downButton;
    public KeyCode FireButton => fireButton;
    public KeyCode ShieldButton => shieldButton;
    public KeyCode DropWeaponButton => dropWeaponButton;
    public float ClickThreshold => clickThreshold;
}