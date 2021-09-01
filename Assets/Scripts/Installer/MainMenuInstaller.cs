using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainMenuSystem>().FromInstance(new MainMenuSystem()).AsSingle();
    }
}