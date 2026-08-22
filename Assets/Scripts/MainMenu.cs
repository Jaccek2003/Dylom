using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }

    public void StartMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    IEnumerator LoadGame()
    {
        AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MenuScene");

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }
}
