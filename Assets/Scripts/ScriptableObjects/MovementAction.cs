using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementActionConfiguration : ScriptableObject
{
    [SerializeField] private string nameAnimation;
    [SerializeField] private float time;

    public string NameAnimation => nameAnimation;

    public float Time => time;
}
