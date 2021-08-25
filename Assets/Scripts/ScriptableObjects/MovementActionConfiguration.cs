using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "movement", fileName = "Kmovement")]
public class MovementActionConfiguration : ScriptableObject
{
    [SerializeField] private string nameAnimation;
    [SerializeField] private float time;

    public string NameAnimation => nameAnimation;

    public float Time => time;
}
