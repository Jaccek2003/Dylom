using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogPanel; // Panel dialogu, który ma siê wyœwietliæ

    private void Start()
    {
        // Na pocz¹tku ukrywamy dialog
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy gracz wszed³ w trigger
        if (other.CompareTag("Player"))
        {
            if (dialogPanel != null)
            {
                dialogPanel.SetActive(true); // W³¹cz panel dialogu
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
                dialogPanel.SetActive(false); // Wy³¹cz panel dialogu
            }
        }
    }
}