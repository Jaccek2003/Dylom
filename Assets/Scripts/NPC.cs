using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Dialog initialDialog;
    private bool initialDialogStarted = false;
    public bool InitialDialogStarted
    {
        get => initialDialogStarted;
        set => initialDialogStarted = value;
    }

    private DialogManager dialogManager;
    private Inventory inventory;

    private bool isMouseOver = false;

    [Serializable]
    public struct DialogItemPair
    {
        public Item item;
        public Dialog dialog;
    }

    public List<DialogItemPair> dialoguesList;  
    private Dictionary<Item, Dialog> dialogues = new Dictionary<Item, Dialog>();
    public List<DialogItemPair> nonAcceptedDialoguesList;
    private Dictionary<Item, Dialog> nonAcceptedDialogues = new Dictionary<Item, Dialog>();

    private void Start()
    {
        dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>().inventory;
        foreach (DialogItemPair pair in dialoguesList)
        {
            dialogues.Add(pair.item, pair.dialog);
        }
        foreach (DialogItemPair pair in nonAcceptedDialoguesList)
        {
            nonAcceptedDialogues.Add(pair.item, pair.dialog);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && isMouseOver)
        {
            if (inventory.IsDragging)
            {
                inventory.RemoveItem(inventory.Item);
                Item draggedItem = inventory.Item;
                if (dialogues.ContainsKey(draggedItem))
                {
                    inventory.IsDragging = false;
                    inventory.RemoveItem(inventory.Item);
                    dialogManager.StartDialog(dialogues[draggedItem]);
                }
                else
                {
                    inventory.IsDragging = false;
                    dialogManager.StartDialog(nonAcceptedDialogues[draggedItem]);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !initialDialogStarted)
        {
            //float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            //if (distanceToPlayer <= talkRange)
            //{
            initialDialogStarted = true;
            dialogManager.StartDialog(initialDialog);
            //}
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
