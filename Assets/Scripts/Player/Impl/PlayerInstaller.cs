using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private BulletManagerConfiguration bulletConfiguration;
    [SerializeField] private PlayerRespawnConfiguration playerRespawnConfiguration;
    [SerializeField] private WeaponManagerConfig weaponManagerConfig;
    
    [SerializeField] private float playerHealth;
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private ShieldSystem inventory;
    [SerializeField] private PlayerMovementStateMachine playerMovementStateMachine;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shield shield;
    [SerializeField] private ShieldMenu shieldMenu;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    
    public override void InstallBindings()
    {
        var playerRespawnSystem = new PlayerRespawnSystem(playerRespawnConfiguration,playerMovement);
        var bulletManager = new BulletManager(bulletConfiguration);
        var playerHealth = new PlayerHealth(this.playerHealth,playerRespawnSystem);
        var weaponManager = new WeaponManager(weaponManagerConfig);
        
        
        Container.Bind<PlayerRespawnSystem>().FromInstance(playerRespawnSystem).AsSingle();
        Container.Bind<WeaponManager>().FromInstance(weaponManager).AsSingle();
        Container.Bind<BulletManager>().FromInstance(bulletManager).AsSingle();
        Container.Bind<IPlayerMovement>().FromInstance(playerMovement).AsSingle();
        Container.Bind<IKeyBindings>().FromInstance(keyBindings).AsSingle();
        Container.Bind<IShieldSystem>().FromInstance(inventory).AsSingle();
        Container.Bind<IShield>().FromInstance(shield).AsSingle();
        Container.Bind<ShieldMenu>().FromInstance(shieldMenu).AsSingle();
        Container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<PlayerAnimatorController>().FromInstance(playerAnimatorController).AsSingle();
        Container.Bind<IPlayerHealth>().FromInstance(playerHealth).AsSingle();
        Container.Bind<PlayerMovementStateMachine>().FromInstance(playerMovementStateMachine).AsSingle();
    }
}