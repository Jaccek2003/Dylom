using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Nazwa sceny, do której chcesz przejść
    public bool shouldChangePlayerPosition = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {

                if(shouldChangePlayerPosition)
                    GameManager.instance.SavePlayerPosition(other.transform.position);

               
                GameManager.instance.menuShouldBeOpen = false;

               
                SceneManager.LoadScene(sceneName);
                
            }
            else
            {
                Debug.LogError("GameManager instance is not set!");
            }
        }
    }
}
