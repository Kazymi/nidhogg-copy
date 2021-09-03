using UnityEngine;
using Zenject;

public class PlayerSystemInstaller : MonoInstaller
{
    [SerializeField] private PlayerWeaponSystem playerWeaponManager;
    [SerializeField] private ShieldSystem inventory;
    [SerializeField] private PlayerRespawnSystem playerRespawnSystem;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    
    public override void InstallBindings()
    {
        Container.Bind<IPlayerWeaponSystem>().FromInstance(playerWeaponManager).AsSingle();
        Container.Bind<IPlayerRespawnSystem>().FromInstance(playerRespawnSystem).AsSingle();
        Container.Bind<IShieldSystem>().FromInstance(inventory).AsSingle();
        Container.Bind<IPlayerAnimatorController>().FromInstance(playerAnimatorController).AsSingle();
    }
}