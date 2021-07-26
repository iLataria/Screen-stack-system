using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ScreenManager screenManager;

    [Zenject.Inject]
    public void Construct(ScreenManager screenManager)
    {
        this.screenManager = screenManager;
    }

    public void LoadScreen<TBaseScreen>() where TBaseScreen : BaseScreen
    {
        screenManager.LoadScreen<TBaseScreen>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScreen<SplashScreen>();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScreen<MainScreen>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadScreen<SettingsScreen>();
        }
    }
}
