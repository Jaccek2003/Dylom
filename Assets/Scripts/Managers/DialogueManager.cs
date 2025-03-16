using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

public class DialogueManager : MonoBehaviour
{
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    public Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    public Animator animator;

    private GameObject continueButton;

    public GameObject dialogPanel;

    private bool canReopenDialogue = false;

    private void Awake()
    {

        lines = new Queue<DialogueLine>();

        continueButton = GameObject.Find("ContinueButton");
        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (isDialogueActive)
        {
            Debug.Log("Zamykam poprzedni dialog...");
            EndDialogue();
        }

        if (dialogPanel == null)
        {
            Debug.LogError("dialogPanel nie jest przypisany w DialogueManager!");
            return;
        }

        Debug.Log($"Rozpoczynam dialog z {dialogue.dialogueLines[0].character.name}");

        isDialogueActive = true;
        canReopenDialogue = false;

        if (continueButton != null)
        {
            continueButton.SetActive(true);
        }

        dialogPanel.SetActive(true);

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();
        Debug.Log(currentLine.line);
        characterIcon.sprite = currentLine.character.icon;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    public void OnContinueButtonClick()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            DisplayNextDialogueLine();
        }
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;

        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }

        StartCoroutine(EnableDialogueReopen());

        Debug.Log("Dialogue ended.");
    }

    IEnumerator EnableDialogueReopen()
    {
        yield return new WaitForSeconds(1.5f);
        canReopenDialogue = true;
    }

    public void TryStartDialogue(Dialogue dialogue)
    {
        if (canReopenDialogue || !isDialogueActive)
        {
            canReopenDialogue = false;
            StartDialogue(dialogue);
        }
    }

    public void ShowDialogueImmediately(Dialogue dialogue)
    {
        canReopenDialogue = false;
        StartDialogue(dialogue);
    }
}