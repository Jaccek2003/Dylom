using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Nazwa sceny, do której chcesz przejœæ\
    public bool shouldChangePlayerPosition = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                // Zapisz pozycjê gracza
                if(shouldChangePlayerPosition)
                    GameManager.instance.SavePlayerPosition(other.transform.position);

                // Ustaw flagê, aby menu nie otwiera³o siê automatycznie
                GameManager.instance.menuShouldBeOpen = false;

                // PrzejdŸ do nowej sceny
                SceneManager.LoadScene(sceneName);

                
            }
            else
            {
                Debug.LogError("GameManager instance is not set!");
            }
        }
    }
}
