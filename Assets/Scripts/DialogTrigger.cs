using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialogu, kt�ry ma si� wy�wietli�

    private void Start()
    {
        // Na pocz�tku ukrywamy dialog
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy gracz wszed� w trigger
        if (other.CompareTag("Player"))
        {
            if (dialogPanel != null)
            {
                dialogPanel.SetActive(true); // W��cz panel dialogu
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ukrywamy dialog, gdy gracz wyjdzie z triggera
        if (other.CompareTag("Player"))
        {
            if (dialogPanel != null)
            {
                dialogPanel.SetActive(false); // Wy��cz panel dialogu
            }
        }
    }
}