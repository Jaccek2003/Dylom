using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Statyczna instancja
    public Vector3 playerPosition = Vector3.zero; // Zapami�tana pozycja gracza
    public bool menuShouldBeOpen = true; // Flaga dla otwarcia menu po wczytaniu sceny

    private void Awake()
    {
        // Upewnij si�, �e istnieje tylko jedna instancja GameManager
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Debug.Log($"instance");
        DontDestroyOnLoad(gameObject); // Zachowaj mi�dzy scenami
    }

    // Metoda zapisywania pozycji gracza
    public void SavePlayerPosition(Vector3 position)
    {
        playerPosition = position;
        Debug.Log($"Zapisano pozycj� gracza: {position}");
    }

    // Metoda przywracania pozycji gracza
    public Vector3 LoadPlayerPosition()
    {
        return playerPosition;
    }
}
