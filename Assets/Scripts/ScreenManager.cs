using UnityEngine;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    private BaseScreen lastScreen;
    private List<BaseScreen> cachedScreens;

    private BaseScreen cachedScreen;

    private void Awake()
    {
        cachedScreens = new List<BaseScreen>();
    }

    public void LoadScreen(BaseScreen screen)
    {
        if (lastScreen && lastScreen.GetId() != screen.GetId() && !lastScreen.IsPoolable())
        {
            Debug.Log($"Destory {lastScreen.gameObject}");
            Destroy(lastScreen.gameObject);
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
                var cached = Instantiate(screen, GameObject.FindGameObjectWithTag("MainCanvas").transform);
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
                lastScreen = Instantiate(screen, GameObject.FindGameObjectWithTag("MainCanvas").transform);
            }

        }

        Debug.Log($"Enable {lastScreen.name}");
        lastScreen.gameObject.SetActive(true);
    }
}
