using System;

public class PlayerHealth : IPlayerHealth
{
    public Action<DamageTarget> PlayerDeath { get; set; }
    public Action PlayerUpdateHealth { get; set; }
    public float MaxHealth { get; }
    public float CurrentHealth { get; private set; }

    public PlayerHealth(float health, PlayerRespawnSystem respawnSystem)
    {
        respawnSystem.RespawnAction += Respawn;
        CurrentHealth = health;
        MaxHealth = health;
    }

    public void TakeDamage(float damage, DamageTarget damageTarget)
    {
        CurrentHealth -= damage;
        PlayerUpdateHealth?.Invoke();
        if (CurrentHealth <= 0)
        {
            PlayerDeath?.Invoke(damageTarget);
        }
    }

    private void Respawn()
    {
        CurrentHealth = MaxHealth;
        PlayerUpdateHealth?.Invoke();
    }
}