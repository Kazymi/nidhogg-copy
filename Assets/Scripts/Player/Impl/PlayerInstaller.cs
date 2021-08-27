using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private KeyBindings keyBindings;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Shield shield;
    [SerializeField] private ShieldMenu shieldMenu;

    public override void InstallBindings()
    {
        Container.Bind<IWeaponDeactivator>().FromInstance(weapon).AsSingle();
        Container.Bind<BulletManager>().FromInstance(bulletManager).AsSingle();
        Container.Bind<IKeyBindings>().FromInstance(keyBindings).AsSingle();
        Container.Bind<IInventory>().FromInstance(inventory).AsSingle();
        Container.Bind<IShield>().FromInstance(shield).AsSingle();
        Container.Bind<ShieldMenu>().FromInstance(shieldMenu).AsSingle();

    }
}