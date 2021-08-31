using System;

public interface IPlayerHealth
{
    public Action<DamageTarget> PlayerDeath { get; set; }
    public Action PlayerTakeDamage { get; set; }
    public void TakeDamage(float damage, DamageTarget damageTarget);

    public float MaxHealth { get; }
    public float CurrentHealth { get; }
}