using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    public GameObject backpackContent; // Obiekt reprezentuj�cy rysunek zawarto�ci plecaka

    void Start()
    {
        // Upewnij si�, �e zawarto�� plecaka jest niewidoczna na pocz�tku
        backpackContent.SetActive(false);
    }

    // Funkcja wywo�ywana po klikni�ciu plecaka
    public void OnBackpackClick()
    {
        backpackContent.SetActive(true); // Pokazuje rysunek zawarto�ci plecaka
    }

    // Funkcja do zamykania rysunku zawarto�ci plecaka
    public void CloseBackpackContent()
    {
        backpackContent.SetActive(false); // Ukrywa rysunek zawarto�ci plecaka
    }
}
