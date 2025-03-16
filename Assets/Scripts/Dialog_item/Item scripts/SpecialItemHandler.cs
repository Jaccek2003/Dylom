using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialItemHandler : MonoBehaviour
{
    public Image specialItemImage; // UI Obrazek przedmiotu
    public TextMeshProUGUI specialThoughtText; // UI Tekst my�li
    public Image darkOverlay; // UI Przyciemnienie ekranu
    public float displayDuration = 4f; // Czas wy�wietlania my�li
    public float overlayAlpha = 0.5f; // Poziom przezroczysto�ci przyciemnienia (0 = niewidoczne, 1 = ca�kowicie czarne)

    private MonoBehaviour playerMovement; // Skrypt odpowiedzialny za ruch gracza
    private bool isDisplaying = false;

    void Start()
    {
        HideSpecialDisplay(); // Ukryj wszystko na start
        playerMovement = FindObjectOfType<MonoBehaviour>(); // Znajd� skrypt ruchu
    }

    public void OnItemPickedUp(GameObject item)
    {
        if (isDisplaying) return; // Je�li ju� co� si� wy�wietla, nic nie r�b

        isDisplaying = true;

        // Pobierz i ustaw obrazek przedmiotu
        SpriteRenderer itemSprite = item.GetComponent<SpriteRenderer>();
        if (itemSprite != null)
        {
            specialItemImage.sprite = itemSprite.sprite;
            specialItemImage.gameObject.SetActive(true);
        }

        // Przyciemnij ekran
        SetOverlayAlpha(overlayAlpha);
        darkOverlay.gameObject.SetActive(true);

        // Ustaw losow� my�l
        specialThoughtText.text = GetRandomThought();
        specialThoughtText.gameObject.SetActive(true);

        // Zablokuj ruch gracza
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Po czasie wy��cz ekran
        Invoke(nameof(HideSpecialDisplay), displayDuration);
    }

    void HideSpecialDisplay()
    {
        specialItemImage.gameObject.SetActive(false);
        specialThoughtText.gameObject.SetActive(false);
        darkOverlay.gameObject.SetActive(false);

        // Odblokuj ruch gracza
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isDisplaying = false;
    }

    void SetOverlayAlpha(float alpha)
    {
        if (darkOverlay != null)
        {
            Color color = darkOverlay.color;
            color.a = alpha;
            darkOverlay.color = color;
        }
    }

    string GetRandomThought()
    {
        string[] thoughts = {
            "Czy to... klucz do czego�?"
        };
        return thoughts[Random.Range(0, thoughts.Length)];
    }
}
