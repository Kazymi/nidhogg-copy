using System;
using UnityEngine;

namespace States.PlayerStates
{
    public class AnimationCondition : PlayerCondition
    {
        private ButtonPressedCondition _buttonPressedConditions;
        private Func<bool> _func;

        public AnimationCondition(Func<bool> func, ButtonPressedCondition buttonPressedCondition)
        {
            _func = func;
            _buttonPressedConditions = buttonPressedCondition;
        }

        public override bool IsConditionSatisfied()
        {
            if (_buttonPressedConditions.IsConditionSatisfied() == false)
            {
                Debug.Log("condition");
                return false;
            }

            Debug.Log(_func.Invoke());
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