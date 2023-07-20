using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private Animator LoadingUI;

    public static Loader Instance { get; private set; }

    public enum ScenesEnum
    {
        MainMenuScene,
        GameScene,
        UpgradesScene,
        TutorialScene,
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(ScenesEnum sceneToLoad)
    {
        LoadingUI.SetTrigger("Start");
        StartCoroutine(Instance.LoadSceneCoroutine(sceneToLoad));
    }

    private IEnumerator LoadSceneCoroutine(ScenesEnum sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad.ToString());
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(1);

        while (!asyncLoad.isDone)
        {
            asyncLoad.allowSceneActivation = true;
            yield return null;
        }
        Time.timeScale = 1;
        LoadingUI.SetTrigger("Loaded");
    }
}
