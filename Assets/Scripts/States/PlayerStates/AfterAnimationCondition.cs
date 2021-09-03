using System;
using System.Collections.Generic;
using UnityEngine;

namespace States.PlayerStates
{
    public class AnimationCondition : PlayerCondition
    {
        private readonly ButtonPressedCondition _buttonPressedConditions;
        private readonly Func<bool> _func;

        public AnimationCondition(Func<bool> func, ButtonPressedCondition buttonPressedCondition)
        {
            _func = func;
            _buttonPressedConditions = buttonPressedCondition;
        }

        public override bool IsConditionSatisfied()
        {
            if (_buttonPressedConditions.IsConditionSatisfied() == false)
            {
                return false;
            }
            return _func.Invoke();
        }

        public override void Initialize()
        {
            base.Initialize();

            _buttonPressedConditions.Initialize();
        }

        public override void DeInitialize()
        {
            base.DeInitialize();

            _buttonPressedConditions.DeInitialize();
        }
    }
}