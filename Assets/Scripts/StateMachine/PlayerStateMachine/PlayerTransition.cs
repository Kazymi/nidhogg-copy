using System;

public class PlayerTransition
{
    public PlayerState StateTo { get; set; }
    public Func<bool> Condition { get; set; }
}