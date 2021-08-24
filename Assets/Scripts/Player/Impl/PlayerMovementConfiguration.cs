
using System;
using UnityEngine;

[Serializable]
    public class PlayerMovementConfiguration
    {
        [SerializeField] private float speed;
        [SerializeField] private float rollingSpeed;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private AnimationCurve rollingCurve;

        public float Speed => speed;

        public float RollingSpeed => rollingSpeed;

        public AnimationCurve RollingCurve => rollingCurve;
        public AnimationCurve JumpCurve => jumpCurve;
    }
