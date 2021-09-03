using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainMenuSystem>().FromInstance(new MainMenuSystem()).AsSingle();
    }
}