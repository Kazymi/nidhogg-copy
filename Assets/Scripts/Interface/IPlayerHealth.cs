  using System;

  public interface IPlayerHealth
  {
      public Action<DamageTarget> PlayerDeath { get; set; }
      public void TakeDamage(float damage, DamageTarget damageTarget);
  }
