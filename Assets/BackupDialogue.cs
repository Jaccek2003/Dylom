using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    void Start()
    {
        dialogueManager = this.GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
