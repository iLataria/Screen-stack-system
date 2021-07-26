using UnityEngine;
using System.Collections.Generic;

public class ScreenManager
{
    private GameObject[] screens;
    private BaseScreen lastScreen;
    private BaseScreen cachedScreen;
    private List<BaseScreen> cachedScreens;
    private Zenject.DiContainer container;

    public ScreenManager(GameObject[] prefabs, Zenject.DiContainer container)
    {
        screens = prefabs;
        cachedScreens = new List<BaseScreen>();
        this.container = container;
    }

    public void LoadScreen<TBaseScreen>() where TBaseScreen : BaseScreen
    {
        BaseScreen screen = null;

        foreach (var prefab in screens)
        {
            TBaseScreen screenComp = prefab.GetComponent<TBaseScreen>();
            if (screenComp)
                screen = container.InstantiatePrefabForComponent<TBaseScreen>(prefab);
        }

        if (lastScreen && lastScreen.GetId() != screen.GetId() && !lastScreen.IsPoolable())
        {
            Debug.Log($"Destory {lastScreen.gameObject}");
            Object.Destroy(lastScreen.gameObject);
            lastScreen = null;
        }
        else if (lastScreen && lastScreen.GetId() != screen.GetId())
        {
            lastScreen.gameObject.SetActive(false);
        }

        if (screen.IsPoolable())
        {
            Debug.Log($"{screen.name} is poolable");
            cachedScreen = cachedScreens.Find((x) => x.GetId() == screen.GetId());

            if (!cachedScreen)
            {
                var cached = Object.Instantiate(screen, GameObject.FindGameObjectWithTag("MainCanvas").transform);
                cachedScreens.Add(cached);
                lastScreen = cached;
            }
            else
            {
                lastScreen = cachedScreen;
            }
        }
        else
        {
            if (!lastScreen || lastScreen.GetId() != screen.GetId())
            {
                lastScreen = Object.Instantiate(screen, GameObject.FindGameObjectWithTag("MainCanvas").transform);
            }
        }

        lastScreen.gameObject.SetActive(true);
    }
}
