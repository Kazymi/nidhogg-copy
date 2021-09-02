using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DisableInteractable : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = false;
    }
}
