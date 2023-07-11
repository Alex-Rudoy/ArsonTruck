using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum ScenesEnum
    {
        MainMenuScene,
        GameLoadingScene,
        GameScene,
        TutorialScene,
    }

    private static ScenesEnum targetScene;

    public static void LoadScene(ScenesEnum sceneToLoad)
    {
        Time.timeScale = 1;
        targetScene = sceneToLoad;
        SceneManager.LoadScene(ScenesEnum.GameLoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
