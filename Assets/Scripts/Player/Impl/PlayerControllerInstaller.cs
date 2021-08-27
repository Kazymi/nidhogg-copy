using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerControllerInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AnimatorConfig animatorConfig;
    [SerializeField] private InputHandler inputHandler;
    public override void InstallBindings()
    {
        var playerAnimatorController = new PlayerAnimatorController(animatorConfig,inputHandler);
        
        Container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<PlayerAnimatorController>().FromInstance(playerAnimatorController).AsSingle();
        Container.Bind<IShieldDeactivator>().FromInstance(playerMovement).AsSingle();
        
    }
}
