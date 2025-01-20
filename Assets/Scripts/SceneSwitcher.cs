using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Nazwa sceny, do kt�rej chcesz przej��\
    public bool shouldChangePlayerPosition = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                // Zapisz pozycj� gracza
                if(shouldChangePlayerPosition)
                    GameManager.instance.SavePlayerPosition(other.transform.position);

                // Ustaw flag�, aby menu nie otwiera�o si� automatycznie
                GameManager.instance.menuShouldBeOpen = false;

                // Przejd� do nowej sceny
                SceneManager.LoadScene(sceneName);

                
            }
            else
            {
                Debug.LogError("GameManager instance is not set!");
            }
        }
    }
}
