using System.Collections.Generic;
using UnityEngine;

public class HealthMenu : MonoBehaviour
{
    [SerializeField] private HealthMenuConfig healthMenuConfigFirstPerson;
    [SerializeField] private HealthMenuConfig healthMenuConfigSecondPerson;

    private readonly Dictionary<PlayerType, HealthMenuConfig> _configs = new Dictionary<PlayerType, HealthMenuConfig>();

    private void Initialize()
    {
        _configs.Add(PlayerType.FirstPlayer, healthMenuConfigFirstPerson);
        _configs.Add(PlayerType.SecondPlayer, healthMenuConfigSecondPerson);
    }

    public void UpdateState(float maxHealth, float currentHealth, PlayerType playerType)
    {
        if (_configs.Count == 0)
        {
            Initialize();
        }

        var config = _configs[playerType];
        config.HealthBar.value = currentHealth / maxHealth;
        config.HealthText.text = $"{currentHealth}/{maxHealth}";
    }
}