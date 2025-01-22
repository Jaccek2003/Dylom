using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Nazwa sceny, do której chcesz przejść
    public bool shouldChangePlayerPosition = true;

    private bool canSwitchScene = true; // Flaga kontrolująca możliwość zmiany sceny
    public float switchCooldown = 1.0f; // Czas oczekiwania po przełączeniu sceny

    private void Start()
    {
        // Ustawienie flaga na "true" w razie potrzeby po załadowaniu sceny
        canSwitchScene = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canSwitchScene && other.CompareTag("Player"))
        {
            // Zablokuj dalsze wyzwalanie zmiany sceny
            canSwitchScene = false;

            // Zapisanie pozycji gracza, jeśli to potrzebne
            if (GameManager.instance != null)
            {
                if (shouldChangePlayerPosition)
                    GameManager.instance.SavePlayerPosition(other.transform.position);

                GameManager.instance.menuShouldBeOpen = false;

                StartCoroutine(SwitchScene());
            }
            else
            {
                Debug.LogError("GameManager instance is not set!");
                canSwitchScene = true;
            }
        }
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(switchCooldown); // Odczekaj czas cooldown
        SceneManager.LoadScene(sceneName);
    }
}
