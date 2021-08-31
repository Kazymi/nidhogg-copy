using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HealthMenuConfig
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthText;

    public Slider HealthBar => healthBar;
    public TMP_Text HealthText => healthText;
}