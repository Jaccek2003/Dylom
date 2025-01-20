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
    public Dialogue dialogue;  // Pierwszy dialog (po podejœciu do NPC)
    private bool hasTriggered = false;  // Zapobiega wielokrotnemu uruchomieniu tego samego dialogu

    public void TriggerDialogue()
    {
        if (!hasTriggered)  // Tylko, jeœli dialog jeszcze nie zosta³ uruchomiony
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            hasTriggered = true;  // Zablokuj ponowne uruchomienie
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)  // Jeœli gracz wejdzie w zasiêg
        {
            TriggerDialogue();  // Uruchom pierwszy dialog
        }
    }
}
