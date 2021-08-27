using System;
using UnityEngine;

[Serializable]
public class ShieldConfig
{
    [SerializeField] private float health;
    [SerializeField] private float regenerationValue;
    [SerializeField] private float timeRegenAfterDamage;

    public float Health => health;

    public float RegenerationValue => regenerationValue;

    public float TimeRegenAfterDamage => timeRegenAfterDamage;
}