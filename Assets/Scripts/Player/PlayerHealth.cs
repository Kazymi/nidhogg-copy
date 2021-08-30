
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerHealth : IPlayerHealth
    {
        private float _currentHealth;

        public Action<DamageTarget> PlayerDeath { get; set; }
        
        public PlayerHealth(float health)
        {
            _currentHealth -= health;
        }

        public void TakeDamage(float damage, DamageTarget damageTarget)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
               PlayerDeath?.Invoke(damageTarget);
            }
        }
      
    }
