using System;

public interface IInputHandler
{
    public event Action Jump;
    public int MovementDirection { get; }
}