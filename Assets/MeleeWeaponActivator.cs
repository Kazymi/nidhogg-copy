using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MeleeWeaponActivator : StateMachineBehaviour
{
    private IPlayerWeaponSystem _playerWeaponSystem;
    private MeleeWeapon _meleeWeapon;

    [Inject]
    private void Construct(IPlayerWeaponSystem playerWeaponSystem)
    {
        _playerWeaponSystem = playerWeaponSystem;
    }
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _meleeWeapon = _playerWeaponSystem.CurrentWeapon.GetComponent<MeleeWeapon>();
        if (_meleeWeapon != null)
        {
            _meleeWeapon.IsActivatedDamageDealer = true;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_meleeWeapon != null)
        {
            _meleeWeapon.IsActivatedDamageDealer = false;
        }
    }
}