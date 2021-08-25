
using System;
using UnityEngine;

[Serializable]
    public class PlayerMovementConfiguration
    {
        [SerializeField] private float speed;
        [SerializeField] private float rollingSpeed;
        [SerializeField] private AnimationCurve jumpCurve;

        public float Speed => speed;

        public float RollingSpeed => rollingSpeed;
        
        public AnimationCurve JumpCurve => jumpCurve;
    }
