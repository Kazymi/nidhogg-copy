
    using System;
    using UnityEngine;
    using Zenject;

    public class HealthMenuSystem : MonoBehaviour
    {
        [SerializeField] private HealthMenu healthMenu;
        [SerializeField] private PlayerType playerType;

        private IPlayerHealth _playerHealth;
        
        [Inject]
        private void Construct(IPlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            playerHealth.PlayerUpdateHealth += PlayerTakeDamage;
        }

        private void Start()
        {
            healthMenu.UpdateState(_playerHealth.MaxHealth,_playerHealth.CurrentHealth,playerType);
        }

        private void PlayerTakeDamage()
        {
            healthMenu.UpdateState(_playerHealth.MaxHealth,_playerHealth.CurrentHealth,playerType);
        }
    }
