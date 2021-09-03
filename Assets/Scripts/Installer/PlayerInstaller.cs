using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] private float playerHealth;
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shield shield;
    [SerializeField] private ShieldMenu shieldMenu;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerRespawnSystem playerRespawnSystem;
    
    public override void InstallBindings()
    {
        var playerHealth = new PlayerHealth(this.playerHealth,playerRespawnSystem);

        Container.Bind<PlayerType>().FromInstance(playerType).AsSingle();
        Container.Bind<IPlayerMovement>().FromInstance(playerMovement).AsSingle();
        Container.Bind<IKeyBindings>().FromInstance(keyBindings).AsSingle();
        Container.Bind<IShield>().FromInstance(shield).AsSingle();
        Container.Bind<IShieldMenu>().FromInstance(shieldMenu).AsSingle();
        Container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<IPlayerHealth>().FromInstance(playerHealth).AsSingle(); }
}