
using System;
using UnityEngine;

[Serializable]
    public class PlayerMovementConfiguration
    {
        [SerializeField] private float speed;
        [SerializeField] private float rollingSpeed;
        [SerializeField] private float shieldSpeed;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float needTimeFallingToRolling;

        [SerializeField] private float rollingTime;
        [SerializeField] private float shieldCrushTime;

        public float RollingTime => rollingTime;

        public float ShieldCrushTime => shieldCrushTime;

        public float NeedTimeFallingToRolling => needTimeFallingToRolling;
        public float Speed => speed;
        
        public float ShieldSpeed => shieldSpeed;
        public float RollingSpeed => rollingSpeed;
        public AnimationCurve JumpCurve => jumpCurve;
    }
