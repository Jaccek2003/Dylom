using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialogu, kt�ry ma si� wy�wietli�
    public Transform player; // Transform gracza (przeci�gnij gracza do tego pola w inspektorze)
    public float talkRange = 3f; // Zasi�g, w kt�rym mo�na rozmawia� z postaci�

    private void Start()
    {
        // Na pocz�tku ukrywamy dialog
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
            // Prze��czamy widoczno�� panelu dialogu
            dialogPanel.SetActive(!dialogPanel.activeSelf);
        }
    }

    // Podchodzenie do postaci (tylko dla ActiveNPC)
    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy gracz wszed� w trigger i czy to ActiveNPC
        if (other.CompareTag("Player") && gameObject.CompareTag("ActiveNPC"))
        {
            // Sprawdzamy odleg�o�� mi�dzy graczem a postaci�
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= talkRange)
            {
                ToggleDialog(); // Pokazujemy dialog, je�li gracz jest wystarczaj�co blisko
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

    // Klikni�cie na posta� (dzia�a dla ActiveNPC i NonActiveNPC)
    private void OnMouseDown()
    {
        // Sprawdzenie odleg�o�ci mi�dzy graczem a obiektem
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= talkRange)
        {
            // Je�li gracz jest wystarczaj�co blisko, prze��czamy dialog
            ToggleDialog();
        }
        else
        {
            Debug.Log("Musisz by� bli�ej, aby porozmawia� z t� osob�.");
        }
    }
}