
using System;
using UnityEngine;

[Serializable]
    public class PlayerMovementConfiguration
    {
        [SerializeField] private float speed;
        [SerializeField] private float rollingSpeed;
        [SerializeField] private float shieldSpeed;
        [SerializeField] private float gravity;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float needTimeFallingToRolling;
        [SerializeField] private Transform groundCheckPosition;
        [SerializeField] private float rollingTime;
        [SerializeField] private float shieldCrushTime;

        public float Speed => speed;

        public float RollingSpeed => rollingSpeed;

        public float ShieldSpeed => shieldSpeed;

        public float Gravity => gravity;

        public AnimationCurve JumpCurve => jumpCurve;

        public float NeedTimeFallingToRolling => needTimeFallingToRolling;
        

        public Transform GroundCheckPosition => groundCheckPosition;

        public float RollingTime => rollingTime;

        public float ShieldCrushTime => shieldCrushTime;
    }
