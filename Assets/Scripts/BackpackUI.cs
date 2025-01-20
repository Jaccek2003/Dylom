using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    public GameObject backpackContent; // Obiekt reprezentuj¹cy rysunek zawartoœci plecaka

    void Start()
    {
        // Upewnij siê, ¿e zawartoœæ plecaka jest niewidoczna na pocz¹tku
        backpackContent.SetActive(false);
    }

    // Funkcja wywo³ywana po klikniêciu plecaka
    public void OnBackpackClick()
    {
        backpackContent.SetActive(true); // Pokazuje rysunek zawartoœci plecaka
    }

    // Funkcja do zamykania rysunku zawartoœci plecaka
    public void CloseBackpackContent()
    {
        backpackContent.SetActive(false); // Ukrywa rysunek zawartoœci plecaka
    }
}
