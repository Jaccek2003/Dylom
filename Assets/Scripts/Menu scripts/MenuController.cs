using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // Panel menu

    public GameObject continueButton; // Przycisk kontynuacji gry
    public GameObject backpackContent; // Panel zawartoœci plecaka



    private void Start()
    {
        // Zapamiêtaj pozycjê gracza ustawion¹ w edytorze


        // Otwórz menu na start gry
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scena_miasto");
        SaveData.instance = new SaveData();

        // Ustaw gracza na jego pocz¹tkowej pozycji (tam, gdzie zosta³ umieszczony w edytorze)

  

        CloseMenu();
    }

    public void Save()
    {

        DataSerializer.Save();
        UpdateContinueButton();
    }

    public void Continue()
    {
        if (DataSerializer.AnySaves())
        {
            DataSerializer.Load();

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


        if (backpackContent != null && backpackContent.activeSelf)
        {
            backpackContent.SetActive(false);
        }

        UpdateContinueButton();
    }

    private void CloseMenu()
    {
        menuPanel.SetActive(false);

    }

    private void UpdateContinueButton()
    {
        if (continueButton != null)
        {
            continueButton.SetActive(DataSerializer.AnySaves());
        }
    }
}
