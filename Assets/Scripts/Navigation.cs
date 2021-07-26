using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Navigation : Zenject.MonoInstaller
{
    [SerializeField] private UiSettings uiSettings;

    public override void InstallBindings()
    {
        Container.Bind<ScreenManager>().FromSubContainerResolve().ByMethod(Install).AsCached().NonLazy();
    }

    private void Install(DiContainer container)
    {
        container
            .Bind<RectTransform>()
            .FromComponentOn(gameObject)
            .AsSingle();

        container.Bind<ScreenManager>().FromNew().AsSingle();
        container.BindInstance(uiSettings.prefabs).WhenInjectedInto<ScreenManager>();
    }
}
