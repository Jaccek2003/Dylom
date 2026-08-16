using UnityEngine;
using UnityEngine.Events;

public class AddItem : MonoBehaviour
{
    public Item Item;
    private InventoryManager inventoryManager;
    private Transform player;
    public float pickupRange = 2.0f;
    public UnityEvent onItemPickedUp;
    private SpecialItemHandler specialItemHandler;

    private ThoughtManager thoughtManager;
    public Thought optionalThought;


       

    private void Start()
    {
        specialItemHandler = FindObjectOfType<SpecialItemHandler>();
        thoughtManager = GameObject.FindGameObjectWithTag("ThoughtManager").GetComponent<ThoughtManager>();
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= pickupRange)
        {
            inventoryManager.inventory.AddItem(Item);


            if (gameObject.CompareTag("Special") && specialItemHandler != null)
            {
                specialItemHandler.OnItemPickedUp(gameObject); 
            }
            if (optionalThought.thoughtLines.Count > 0)
                thoughtManager.StartThought(optionalThought);
            onItemPickedUp.Invoke();
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Musisz byæ bli¿ej, aby podnieœæ ten przedmiot.");
        }
    }
}
