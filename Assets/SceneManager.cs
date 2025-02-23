using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SceneEntry
{
    public string sceneName;
    public GameObject sceneObject;
}

public class SceneManager : MonoBehaviour
{
    public List<SceneEntry> scenes = new List<SceneEntry>();

    // Start is called before the first frame update
    public void LoadSceneByName(string name)
    {
        foreach (SceneEntry entry in scenes)
        {
            entry.sceneObject.SetActive(false);
        }

        // Look for the scene name in the list
        SceneEntry scene = scenes.Find(s => s.sceneName == name);
        if (scene.sceneObject != null)
        {
            scene.sceneObject.SetActive(true);
        }
        else
        {
            Debug.Log("Scene with name: " + name + " does not exist.");
        }
    }
}
