using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RespawnAnimation : StateMachineBehaviour
{
    private PlayerRespawnSystem _respawnSystem;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _respawnSystem.Respawn();
    }

    [Inject]
    private void Construct(PlayerRespawnSystem respawnSystem)
    {
        _respawnSystem = respawnSystem;
    }
}