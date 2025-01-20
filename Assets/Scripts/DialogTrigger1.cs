using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;  // Pierwszy dialog (po podej�ciu do NPC)
    private bool hasTriggered = false;  // Zapobiega wielokrotnemu uruchomieniu tego samego dialogu

    public void TriggerDialogue()
    {
        if (!hasTriggered)  // Tylko, je�li dialog jeszcze nie zosta� uruchomiony
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            hasTriggered = true;  // Zablokuj ponowne uruchomienie
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)  // Je�li gracz wejdzie w zasi�g
        {
            TriggerDialogue();  // Uruchom pierwszy dialog
        }
    }
}
