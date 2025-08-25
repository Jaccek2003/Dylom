using UnityEngine;
using UnityEngine.Events;

public class AddItem : MonoBehaviour
{
    public Item Item;
    private RotationHandler rotationHandler;
    private Transform player;
    public float pickupRange = 2.0f;

    public UnityEvent onItemPickedUp;

    private SpecialItemHandler specialItemHandler; // Dodajemy referencjê do obs³ugi "specjalnych" przedmiotów

    private void Start()
    {
        specialItemHandler = FindObjectOfType<SpecialItemHandler>(); // ZnajdŸ skrypt w scenie
        rotationHandler = FindObjectOfType<RotationHandler>(true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= pickupRange)
        {
            if(Item != null)
            {
                rotationHandler.AddItem(Item);
                InventoryManager.Instance.AddItem(Item.name); // Dodaj do ekwipunku
            }
            if (gameObject.CompareTag("Special") && specialItemHandler != null)
            {
                specialItemHandler.OnItemPickedUp(gameObject); // Wywo³aj obs³ugê dla specjalnych przedmiotów
            }
            onItemPickedUp.Invoke();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Musisz byæ bli¿ej, aby podnieœæ ten przedmiot.");
        }
    }
}
