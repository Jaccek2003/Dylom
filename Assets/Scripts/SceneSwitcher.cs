using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Experimental.AI;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;
    private SceneManager sceneManager;

    public Vector3 newPos;

    private void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.position = newPos;
            sceneManager.LoadSceneByName(sceneName);
        }
    }

}
