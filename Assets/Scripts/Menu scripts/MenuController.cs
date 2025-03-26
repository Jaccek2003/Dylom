using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // Panel menu
    public Movement playerMovement; // Odwo�anie do skryptu gracza
    public GameObject continueButton; // Przycisk kontynuacji gry
    public GameObject backpackContent; // Panel zawarto�ci plecaka

    private Vector3 initialPosition; // Przechowuje pocz�tkow� pozycj� gracza

    private void Start()
    {
        // Zapami�taj pozycj� gracza ustawion� w edytorze
        initialPosition = playerMovement.transform.position;

        // Otw�rz menu na start gry
        OpenMenu();
    }

    private void Update()
    {
        // Otw�rz lub zamknij menu po naci�ni�ciu klawisza Escape
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void NewGame()
    {
        SaveData.instance = new SaveData();

        // Ustaw gracza na jego pocz�tkowej pozycji (tam, gdzie zosta� umieszczony w edytorze)
        playerMovement.transform.position = initialPosition;
  

        CloseMenu();
    }

    public void Save()
    {
        playerMovement.Save();
        DataSerializer.Save();
        UpdateContinueButton();
    }

    public void Continue()
    {
        if (DataSerializer.AnySaves())
        {
            DataSerializer.Load();
            playerMovement.Load();
            CloseMenu();
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ToggleMenu()
    {
        if (menuPanel.activeSelf)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        menuPanel.SetActive(true);
        playerMovement.enabled = false;

        if (backpackContent != null && backpackContent.activeSelf)
        {
            backpackContent.SetActive(false);
        }

        UpdateContinueButton();
    }

    private void CloseMenu()
    {
        menuPanel.SetActive(false);
        playerMovement.enabled = true;
    }

    private void UpdateContinueButton()
    {
        if (continueButton != null)
        {
            continueButton.SetActive(DataSerializer.AnySaves());
        }
    }
}
