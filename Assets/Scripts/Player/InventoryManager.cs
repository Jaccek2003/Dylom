using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    public Canvas inventoryCanvas;

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
    }
}
