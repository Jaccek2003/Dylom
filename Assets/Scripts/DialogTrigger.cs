using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialogu, który ma siê wyœwietliæ
    public Transform player; // Transform gracza (przeci¹gnij gracza do tego pola w inspektorze)
    public float talkRange = 3f; // Zasiêg, w którym mo¿na rozmawiaæ z postaci¹

    private void Start()
    {
        // Na pocz¹tku ukrywamy dialog
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    // Funkcja do pokazywania/ukrywania dialogu
    private void ToggleDialog()
    {
        if (dialogPanel != null)
        {
            // Prze³¹czamy widocznoœæ panelu dialogu
            dialogPanel.SetActive(!dialogPanel.activeSelf);
        }
    }

    // Podchodzenie do postaci (tylko dla ActiveNPC)
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy gracz wszed³ w trigger i czy to ActiveNPC
        if (other.CompareTag("Player") && gameObject.CompareTag("ActiveNPC"))
        {
            // Sprawdzamy odleg³oœæ miêdzy graczem a postaci¹
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= talkRange)
            {
                ToggleDialog(); // Pokazujemy dialog, jeœli gracz jest wystarczaj¹co blisko
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ukrywamy dialog, gdy gracz wyjdzie z triggera (tylko dla ActiveNPC)
        if (other.CompareTag("Player") && gameObject.CompareTag("ActiveNPC"))
        {
            ToggleDialog(); // Ukrywamy dialog
        }
    }

    // Klikniêcie na postaæ (dzia³a dla ActiveNPC i NonActiveNPC)
    private void OnMouseDown()
    {
        // Sprawdzenie odleg³oœci miêdzy graczem a obiektem
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= talkRange)
        {
            // Jeœli gracz jest wystarczaj¹co blisko, prze³¹czamy dialog
            ToggleDialog();
        }
        else
        {
            Debug.Log("Musisz byæ bli¿ej, aby porozmawiaæ z t¹ osob¹.");
        }
    }
}