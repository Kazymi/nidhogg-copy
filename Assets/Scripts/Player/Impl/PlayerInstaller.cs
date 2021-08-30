using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private float playerHealth;
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shield shield;
    [SerializeField] private ShieldMenu shieldMenu;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    
    public override void InstallBindings()
    {
        
        Container.Bind<BulletManager>().FromInstance(bulletManager).AsSingle();
        Container.Bind<IPlayerMovement>().FromInstance(playerMovement).AsSingle();
        Container.Bind<IKeyBindings>().FromInstance(keyBindings).AsSingle();
        Container.Bind<IInventory>().FromInstance(inventory).AsSingle();
        Container.Bind<IShield>().FromInstance(shield).AsSingle();
        Container.Bind<ShieldMenu>().FromInstance(shieldMenu).AsSingle();
        Container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<PlayerAnimatorController>().FromInstance(playerAnimatorController).AsSingle();

        var playerHealth = new PlayerHealth(this.playerHealth);

        Container.Bind<IPlayerHealth>().FromInstance(playerHealth).AsSingle();
    }
}