
    using System;
    using UnityEngine;
    using Zenject;

    public class HealthMenuSystem : MonoBehaviour
    {
        [SerializeField] private HealthMenu healthMenu;

        private PlayerType _playerType;
        private IPlayerHealth _playerHealth;
        
        [Inject]
        private void Construct(IPlayerHealth playerHealth, PlayerType playerType)
        {
            _playerHealth = playerHealth;
            playerHealth.PlayerUpdateHealth += PlayerTakeDamage;
            _playerType = playerType;
        }

        private void Start()
        {
            healthMenu.UpdateState(_playerHealth.MaxHealth,_playerHealth.CurrentHealth,_playerType);
        }

        private void PlayerTakeDamage()
        {
            healthMenu.UpdateState(_playerHealth.MaxHealth,_playerHealth.CurrentHealth,_playerType);
        }
    }
