using System;
using UnityEngine;

    public class AfterFallCondition : PlayerCondition
    {
        private readonly Func<bool> _func;
        private readonly float _needTimeToFall;
        private float _currentTimeFall;

        public AfterFallCondition(Func<bool> func, float needTimeToFall)
        {
            _func = func;
            _needTimeToFall = needTimeToFall;
        }

        public override bool IsConditionSatisfied()
        {
            _currentTimeFall += Time.deltaTime;
            if (_func.Invoke())
            {
                if (_needTimeToFall < _currentTimeFall)
                {
                    return true;
                }
            }

            return false;
        }

        public override void Initialize()
        {
            base.Initialize();
            _currentTimeFall = 0;
        }
    }
