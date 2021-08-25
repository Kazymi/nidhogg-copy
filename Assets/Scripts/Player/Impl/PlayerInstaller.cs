using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private BulletManager bulletManager;

    public override void InstallBindings()
    {
        Container.Bind<BulletManager>().FromInstance(bulletManager).AsSingle();
        Container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<IKeyBindings>().FromInstance(keyBindings).AsSingle();
        Container.Bind<PlayerAnimatorController>().FromInstance(playerAnimatorController).AsSingle();
    }
}