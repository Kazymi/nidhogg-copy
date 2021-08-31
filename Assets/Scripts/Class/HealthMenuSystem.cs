
    using System;
    using UnityEngine;
    using Zenject;

    public class HealthMenuSystem : MonoBehaviour
    {
        [SerializeField] private HealthMenu healthMenu;
        
        private IPlayerHealth _playerHealth;
        
        [Inject]
        private void Construct(IPlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            playerHealth.PlayerTakeDamage += PlayerTakeDamage;
        }

        private void Start()
        {
            healthMenu.UpdateState(_playerHealth.MaxHealth,_playerHealth.CurrentHealth);
        }

        private void PlayerTakeDamage()
        {
            healthMenu.UpdateState(_playerHealth.MaxHealth,_playerHealth.CurrentHealth);
        }
    }
