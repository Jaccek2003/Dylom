using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class Loading : MonoBehaviour
{
    public string sceneToLoad;
    AsyncOperation loadingOperation;
    public Slider progressBar;


    void Start()
    {
        loadingOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
    }

    void Update()
    {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }
}
