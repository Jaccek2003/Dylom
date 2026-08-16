using System;
using System.Collections.Generic;
using UnityEngine;

public class AcceptItem : MonoBehaviour
{
    [Serializable]
    public struct DialougeItemPair
    {
        public string name;
        public Dialogue dialogue;
    }

    public DialogueManager dialogueManager;

    public List<string> acceptedItems;  // Lista akceptowanych przedmiotów
    public RotationHandler rotationHandler;  // Obsługa przeciągania przedmiotów
    public List<DialougeItemPair> dialoguesList;  // Dialog wyświetlany po zaakceptowaniu przedmiotu
    private Dictionary<string, Dialogue> dialogues = new Dictionary<string, Dialogue>();
    public List<DialougeItemPair> nonAcceptedDialoguesList;
    private Dictionary<string, Dialogue> nonAcceptedDialogues = new Dictionary<string, Dialogue>();
    private bool isMouseOver = false;  // Flaga do sprawdzania, czy myszka jest nad NPC

    // Referencja do obrazu UI, który ma być usunięty
    public GameObject uiImage;

    private void Start()
    {
        foreach (DialougeItemPair pair in dialoguesList)
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
        // Sprawdzanie, czy przedmiot jest przeciągany nad NPC i został upuszczony
        if (Input.GetMouseButtonUp(0) && isMouseOver)
        {
            if (rotationHandler.isDragging)
            {
                string draggedItem = rotationHandler.itemName;
                Debug.Log(rotationHandler.itemName);
                if (acceptedItems.Contains(draggedItem))
                {
                    // Usuń obraz UI
                    if (uiImage != null)
                    {
                        uiImage.SetActive(false);
                    }

                    // Zresetuj stan przeciągania
                    rotationHandler.isDragging = false;
                    rotationHandler.RemoveItem(rotationHandler.itemName);

                    // Uruchom dialog związany z przedmiotem
                    dialogueManager.StartDialogue(dialogues[rotationHandler.itemName]);
                }
                else
                {
                    // Zresetuj stan przeciągania
                    rotationHandler.isDragging = false;

                    // Uruchom dialog związany z przedmiotem
                    dialogueManager.StartDialogue(nonAcceptedDialogues[rotationHandler.itemName]);
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
        isMouseOver = false;  // Myszka opuściła NPC
    }
}
