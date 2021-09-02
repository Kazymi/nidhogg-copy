using System;
using UnityEngine;

public interface IPlayerMovement
{
    public Action<bool> DefaultMovement { get; set; }
    public PlayerMovementConfiguration PlayerMovementConfiguration { get; }
    public Rigidbody Rigidbody { get; }
    public bool IsJumped { get; set; }
    public bool IsGrounded { get; }
    public void Rolling();
    public void Move(float speedRedux);
    public void StartJump();
    public void SetPosition(Transform position);
    public void MoveUpdate();
}