using System;
using UnityEngine;

[Serializable]
public class AnimatorConfig
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speedRunAnimation;

    public Animator PlayerAnimator => playerAnimator;

    public float SpeedRunAnimation => speedRunAnimation;
}