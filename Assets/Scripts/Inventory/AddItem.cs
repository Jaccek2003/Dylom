using UnityEngine;

public class AddItem : MonoBehaviour
{
    public Item Item;
    public RotationHandler rotationHandler;
    public Transform player;
    public float pickupRange = 2.0f;

    private SpecialItemHandler specialItemHandler; // Dodajemy referencjê do obs³ugi "specjalnych" przedmiotów

    private void Start()
    {
        specialItemHandler = FindObjectOfType<SpecialItemHandler>(); // ZnajdŸ skrypt w scenie
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
                specialItemHandler.OnItemPickedUp(gameObject); // Wywo³aj obs³ugê dla specjalnych przedmiotów
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Musisz byæ bli¿ej, aby podnieœæ ten przedmiot.");
        }
    }
}
