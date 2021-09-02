using System;
using System.Collections.Generic;
using UnityEngine;

namespace States.PlayerStates
{
    public class AnimationCondition : PlayerCondition
    {
        private List<ButtonPressedCondition> _buttonPressedConditions;
        private Func<bool> _func;
        
        public AnimationCondition(Func<bool> func, List<ButtonPressedCondition> buttonPressedCondition)
        {
            _func = func;
            _buttonPressedConditions = buttonPressedCondition;
        }
        public override bool IsConditionSatisfied()
        {
            foreach (var buttonPressed in _buttonPressedConditions)
            {
                if (buttonPressed.IsConditionSatisfied() == false)
                {
                    Debug.Log("condition");
                    return false;
                }
            }

            Debug.Log(_func.Invoke());
            return _func.Invoke();
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var buttonPressed in _buttonPressedConditions)
            {
               buttonPressed.Initialize();
            }
        }

        public override void DeInitialize()
        {
            base.DeInitialize();
            foreach (var buttonPressed in _buttonPressedConditions)
            {
                buttonPressed.DeInitialize();
            }
        }
    }
}