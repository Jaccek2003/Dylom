using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // Panel menu
    public Movement playerMovement; // Odwo³anie do skryptu gracza
    public GameObject continueButton; // Przycisk kontynuacji gry
    public GameObject backpackContent; // Panel zawartoœci plecaka
    public Vector3 startPosition = new Vector3(0, 1, 0); // Pocz¹tkowa pozycja gracza

    private void Start()
    {
        // Jeœli GameManager istnieje, ustaw pozycjê gracza
        if (GameManager.instance != null)
        {
            Vector3 savedPosition = GameManager.instance.LoadPlayerPosition();
            if (savedPosition != Vector3.zero)
            {
                playerMovement.transform.position = savedPosition;
            }

            // SprawdŸ flagê menu
            if (!GameManager.instance.menuShouldBeOpen)
            {
                GameManager.instance.menuShouldBeOpen = true; // Resetuj flagê
                CloseMenu();
                return;
            }
        }

        // Otwórz menu, jeœli flaga tego wymaga
        OpenMenu();
    }

    private void Update()
    {
        // Otwórz lub zamknij menu po naciœniêciu klawisza Escape
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void NewGame()
    {
        SaveData.instance = new SaveData();
        playerMovement.transform.position = startPosition;
        playerMovement.ResetMovement();
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
