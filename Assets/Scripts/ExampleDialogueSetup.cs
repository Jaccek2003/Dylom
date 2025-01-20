using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogueSetup : MonoBehaviour
{
    public DialogueCharacter playerCharacter;  // Przeci�gnij Player tutaj
    public DialogueCharacter npcCharacter;     // Przeci�gnij Posta� 1 tutaj

    private Dialogue exampleDialogue;

    private void Start()
    {
        // Tworzymy dialog z ustalonymi liniami
        exampleDialogue = new Dialogue
        {
            dialogueLines = new List<DialogueLine>
            {
                new DialogueLine
                {
                    character = playerCharacter,
                    line = "Cze��, Idziesz z nami na boisko?"
                },
                new DialogueLine
                {
                    character = npcCharacter,
                    line = " ... "
                },
                new DialogueLine
                {
                    character = playerCharacter,
                    line = "Nie umiesz gada�?"
                },
                new DialogueLine
                {
                    character = npcCharacter,
                    line = " ... "
                },
                new DialogueLine
                {
                    character = playerCharacter,
                    line = " mhm... po prostu do nas wpadnij jak b�dziesz mia� czas, dobrze ?"
                },
            }
        };

        // Rozpocznij dialog
        DialogueManager.Instance.StartDialogue(exampleDialogue);
    }
}

