using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialItemHandler : MonoBehaviour
{
    public Image specialItemImage; // UI Obrazek przedmiotu
    public TextMeshProUGUI specialThoughtText; // UI Tekst myœli
    public Image darkOverlay; // UI Przyciemnienie ekranu
    public float displayDuration = 4f; // Czas wyœwietlania myœli
    public float overlayAlpha = 0.5f; // Poziom przezroczystoœci przyciemnienia (0 = niewidoczne, 1 = ca³kowicie czarne)

    private MonoBehaviour playerMovement; // Skrypt odpowiedzialny za ruch gracza
    private bool isDisplaying = false;

    void Start()
    {
        HideSpecialDisplay(); // Ukryj wszystko na start
        playerMovement = FindObjectOfType<MonoBehaviour>(); // ZnajdŸ skrypt ruchu
    }

    public void OnItemPickedUp(GameObject item)
    {
        if (isDisplaying) return; // Jeœli ju¿ coœ siê wyœwietla, nic nie rób

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

        // Ustaw losow¹ myœl
        specialThoughtText.text = GetRandomThought();
        specialThoughtText.gameObject.SetActive(true);

        // Zablokuj ruch gracza
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Po czasie wy³¹cz ekran
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
            "Czy to... klucz do czegoœ?"
        };
        return thoughts[Random.Range(0, thoughts.Length)];
    }
}
