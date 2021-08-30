using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var mainMenuSystem = new MainMenuSystem();
        Container.Bind<MainMenuSystem>().FromInstance(mainMenuSystem).AsSingle();

    }
}