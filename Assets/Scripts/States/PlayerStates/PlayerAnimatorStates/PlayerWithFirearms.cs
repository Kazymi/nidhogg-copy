using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class PlayerWithFirearms : State
    {
        private int _weaponHash = Animator.StringToHash("Weapon");
        
        private Animator _animator;
        public override void OnStateEnter()
        {
            _animator.SetBool(_weaponHash,true);
        }

        public PlayerWithFirearms(Animator animator)
        {
            _animator = animator;
        }
    }
   
}