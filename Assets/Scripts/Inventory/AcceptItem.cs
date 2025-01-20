using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AcceptItem : MonoBehaviour
{
    [Serializable]
    public struct DialougeItemPair
    {
        public string name;
        public Dialogue dialogue;
    }

    public List<string> acceptedItems;  // Lista akceptowanych przedmiotów
    public RotationHandler rotationHandler;  // Obs³uga przeci¹gania przedmiotów
    public List<DialougeItemPair> dialoguesList;  // Dialog wyœwietlany po zaakceptowaniu przedmiotu
    private Dictionary<string, Dialogue> dialogues = new Dictionary<string, Dialogue>();
    public List<DialougeItemPair> nonAcceptedDialoguesList;
    private Dictionary<string, Dialogue> nonAcceptedDialogues = new Dictionary<string, Dialogue>();
    private bool isMouseOver = false;  // Flaga dosprawdzania, czy myszka jest nad NPC

    private void Start()
    {
        foreach(DialougeItemPair pair in dialoguesList)
        {
            Debug.Log(pair.name + " " + pair.dialogue.ToString());
            dialogues.Add(pair.name, pair.dialogue);
        }
        foreach (DialougeItemPair pair in nonAcceptedDialoguesList)
        {
            nonAcceptedDialogues.Add(pair.name, pair.dialogue);
        }
    }

    void Update()
    {
        // Sprawdzanie, czy przedmiot jest przeci¹gany nad NPC i zosta³ upuszczony
        if (Input.GetMouseButtonUp(0) && isMouseOver)
        {
            if (rotationHandler.isDragging)
            {
                string draggedItem = rotationHandler.itemName;
                Debug.Log(rotationHandler.itemName);
                if (acceptedItems.Contains(draggedItem))
                {
                    

                    // Zresetuj stan przeci¹gania
                    rotationHandler.isDragging = false;
                    rotationHandler.RemoveItem(rotationHandler.itemName);
                    // Uruchom dialog zwi¹zany z przedmiotem
                    DialogueManager.Instance.StartDialogue(dialogues[rotationHandler.itemName]);
                }
                else
                {
                    // Zresetuj stan przeci¹gania
                    rotationHandler.isDragging = false;
                    //rotationHandler.RemoveItem(rotationHandler.itemName);
                    // Uruchom dialog zwi¹zany z przedmiotem
                    DialogueManager.Instance.StartDialogue(nonAcceptedDialogues[rotationHandler.itemName]);
                }
            }
        }
    }

    void OnMouseEnter()
    {
        isMouseOver = true;  // Myszka nad NPC
    }

    void OnMouseExit()
    {
        isMouseOver = false;  // Myszka opuœci³a NPC
    }
}

