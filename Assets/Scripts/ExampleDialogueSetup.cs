using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogueSetup : MonoBehaviour
{
    public DialogueCharacter playerCharacter;  // Przeci¹gnij Player tutaj
    public DialogueCharacter npcCharacter;     // Przeci¹gnij Postaæ 1 tutaj

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
                    line = "Czeœæ, Idziesz z nami na boisko?"
                },
                new DialogueLine
                {
                    character = npcCharacter,
                    line = " ... "
                },
                new DialogueLine
                {
                    character = playerCharacter,
                    line = "Nie umiesz gadaæ?"
                },
                new DialogueLine
                {
                    character = npcCharacter,
                    line = " ... "
                },
                new DialogueLine
                {
                    character = playerCharacter,
                    line = " mhm... po prostu do nas wpadnij jak bêdziesz mia³ czas, dobrze ?"
                },
            }
        };

        // Rozpocznij dialog
        DialogueManager.Instance.StartDialogue(exampleDialogue);
    }
}

