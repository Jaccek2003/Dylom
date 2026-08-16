

public class AddItemEvent : BaseEvent
{
    public Item item;

    public override void Exectute()
    {
        FindObjectOfType<InventoryManager>().inventory.AddItem(item);
    }
}
