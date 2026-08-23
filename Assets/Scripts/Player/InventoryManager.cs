using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    public Canvas inventoryCanvas, flowerCanvas;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (ProgressManager.Instance.WasBackpackTaken)
            if (Input.GetKeyDown(KeyCode.Tab) && !inventory.IsRotating) 
            {
                ToggleInventory();
            }
    }

    private void ToggleInventory()
    {
        inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);
        flowerCanvas.gameObject.SetActive(!flowerCanvas.gameObject.activeSelf);
    }
}
