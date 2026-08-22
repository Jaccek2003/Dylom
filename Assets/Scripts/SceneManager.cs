using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SceneEntry
{
    public string sceneName;
    public GameObject sceneObject;
    public AudioClip additionalMusic;
}

public class SceneManager : MonoBehaviour
{
    public List<SceneEntry> scenes = new List<SceneEntry>();

    public AudioSource additionalMusic;

    public void LoadSceneByName(string name)
    {
        foreach (SceneEntry entry in scenes)
        {
            entry.sceneObject.SetActive(false);
        }

        SceneEntry scene = scenes.Find(s => s.sceneName == name);
        if (scene.sceneObject != null)
        {
            scene.sceneObject.SetActive(true);
            additionalMusic.clip = scene.additionalMusic;
            if(scene.additionalMusic == null)
                additionalMusic.Stop();
            else
                additionalMusic.Play();
        }
        else
        {
            Debug.Log("Scene with name: " + name + " does not exist.");
        }
    }
}
