using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private BulletManagerConfiguration bulletConfiguration;
    
    [SerializeField] private float playerHealth;
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerMovementStateMachine playerMovementStateMachine;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shield shield;
    [SerializeField] private ShieldMenu shieldMenu;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    
    public override void InstallBindings()
    {
        var bulletManager = new BulletManager(bulletConfiguration);
        var playerHealth = new PlayerHealth(this.playerHealth);
        
        Container.Bind<BulletManager>().FromInstance(bulletManager).AsSingle();
        Container.Bind<IPlayerMovement>().FromInstance(playerMovement).AsSingle();
        Container.Bind<IKeyBindings>().FromInstance(keyBindings).AsSingle();
        Container.Bind<IInventory>().FromInstance(inventory).AsSingle();
        Container.Bind<IShield>().FromInstance(shield).AsSingle();
        Container.Bind<ShieldMenu>().FromInstance(shieldMenu).AsSingle();
        Container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<PlayerAnimatorController>().FromInstance(playerAnimatorController).AsSingle();
        Container.Bind<IPlayerHealth>().FromInstance(playerHealth).AsSingle();
        Container.Bind<PlayerMovementStateMachine>().FromInstance(playerMovementStateMachine).AsSingle();
    }
}