using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Experimental.AI;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Nazwa sceny, do której chcesz przejść
    private SceneManager sceneManager;

    public Vector3 newPos;


    private void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (GameManager.instance != null)
            {
                other.transform.position = newPos;
                sceneManager.LoadSceneByName(sceneName);

            }
            else
            {
                Debug.LogError("GameManager instance is not set!");

            }
        }
    }

}
