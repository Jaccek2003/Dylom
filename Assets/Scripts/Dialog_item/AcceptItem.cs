using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AcceptItem : MonoBehaviour
{
    [Serializable]
    public struct DialougeItemPair
    {
        public string name;
        public Dialogue dialogue;
    }

    public DialogueManager dialogueManager;
    public RotationHandler rotationHandler;  // Obsługa przeciągania przedmiotów
    public List<DialougeItemPair> dialoguesList;  // Dialog wyświetlany po zaakceptowaniu przedmiotu
    private Dictionary<string, Dialogue> dialogues = new Dictionary<string, Dialogue>();
    public List<DialougeItemPair> nonAcceptedDialoguesList;
    private Dictionary<string, Dialogue> nonAcceptedDialogues = new Dictionary<string, Dialogue>();
    private bool isMouseOver = false;  // Flaga do sprawdzania, czy myszka jest nad NPC

    public UnityEvent<string> onItemConsume;

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
                if (dialogues.ContainsKey(draggedItem))
                {
                    // Usuń obraz UI
                    if (uiImage != null)
                    {
                        uiImage.SetActive(false);
                    }

                    // Zresetuj stan przeciągania
                    rotationHandler.isDragging = false;
                    rotationHandler.RemoveItem(rotationHandler.itemName);
                    TriggerAcceptedByKey(rotationHandler.itemName);

                    // Uruchom dialog związany z przedmiotem
                    dialogueManager.StartDialogue(dialogues[rotationHandler.itemName]);
                }
                else
                {
                    // Zresetuj stan przeciągania
                    rotationHandler.isDragging = false;
                    TriggerNotAcceptedByKey(rotationHandler.itemName);
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


    public List<UnityEvent<string>> onAcceptedItem = new List<UnityEvent<string>>();
    public List<UnityEvent<string>> onNotAcceptedItem = new List<UnityEvent<string>>();

    private void OnValidate()
    {
        while (onAcceptedItem.Count < dialoguesList.Count)
            onAcceptedItem.Add(new UnityEvent<string>());

        while (onAcceptedItem.Count > dialoguesList.Count)
            onAcceptedItem.RemoveAt(onAcceptedItem.Count - 1);

        while (onNotAcceptedItem.Count < nonAcceptedDialoguesList.Count)
            onNotAcceptedItem.Add(new UnityEvent<string>());

        while (onNotAcceptedItem.Count > nonAcceptedDialoguesList.Count)
            onNotAcceptedItem.RemoveAt(onNotAcceptedItem.Count - 1);
    }

    public void TriggerAcceptedByKey(string key)
    {
        for (int i = 0; i < dialoguesList.Count; i++)
        {
            if (dialoguesList[i].name == key)
            {
                onAcceptedItem[i]?.Invoke(key);
            }
        }
    }

    public void TriggerNotAcceptedByKey(string key)
    {
        for (int i = 0; i < nonAcceptedDialoguesList.Count; i++)
        {
            if (nonAcceptedDialoguesList[i].name == key)
            {
                onNotAcceptedItem[i]?.Invoke(key);
            }
        }
    }
}
