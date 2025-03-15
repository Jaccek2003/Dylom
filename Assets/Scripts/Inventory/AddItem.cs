using UnityEngine;

public class AddItem : MonoBehaviour
{
    public Item Item;
    public RotationHandler rotationHandler;
    public Transform player;
    public float pickupRange = 2.0f;

    private SpecialItemHandler specialItemHandler; // Dodajemy referencj� do obs�ugi "specjalnych" przedmiot�w

    private void Start()
    {
        specialItemHandler = FindObjectOfType<SpecialItemHandler>(); // Znajd� skrypt w scenie
    }

    private void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= pickupRange)
        {
            rotationHandler.AddItem(Item);
            InventoryManager.Instance.AddItem(Item.name); // Dodaj do ekwipunku

            if (gameObject.CompareTag("Special") && specialItemHandler != null)
            {
                specialItemHandler.OnItemPickedUp(gameObject); // Wywo�aj obs�ug� dla specjalnych przedmiot�w
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Musisz by� bli�ej, aby podnie�� ten przedmiot.");
        }
    }
}
