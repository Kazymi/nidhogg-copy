
using System;
using UnityEngine;

[Serializable]
    public class PlayerMovementConfiguration
    {
        [SerializeField] private float speed;
        [SerializeField] private float rollingSpeed;
        [SerializeField] private float shieldSpeed;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float toFallingTime;

        public float ToFallingTime => toFallingTime;
        public float Speed => speed;
        
        public float ShieldSpeed => shieldSpeed;
        public float RollingSpeed => rollingSpeed;
        public AnimationCurve JumpCurve => jumpCurve;
    }
