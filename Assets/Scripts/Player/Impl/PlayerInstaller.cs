using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private CharacterController characterController;

    public override void InstallBindings()
    {
        Container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<IKeyBindings>().FromInstance(keyBindings).AsSingle();
        Container.Bind().FromInstance(characterController).AsSingle();
    }
}