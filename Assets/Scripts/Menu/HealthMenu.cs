using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMenu : MonoBehaviour
{
   [SerializeField] private HealthMenuConfig healthMenuConfig;

   public void UpdateState(float maxHealth, float currentHealth)
   {
      healthMenuConfig.HealthBar.value = currentHealth/maxHealth;
      healthMenuConfig.HealthText.text = $"{currentHealth}/{maxHealth}";
   }
}
