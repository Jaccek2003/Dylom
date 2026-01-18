using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

[System.Serializable]
[CreateAssetMenu]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
[CreateAssetMenu]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
    public AudioClip audioClip;
}

[System.Serializable]
[CreateAssetMenu]
public class Dialogue
{
    public string id;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}




public class DialogueManager : MonoBehaviour
{
    [Serializable]
    public class EndDialogueEvent
    {
        public string id;
        public UnityEvent unityEvent;
    }
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

    public List<EndDialogueEvent> endDialogueEventList;

    private Dictionary<string, UnityEvent> unityEventsMap = new Dictionary<string, UnityEvent>();

    private string currentDialogueID;

    private void Start()
    {
        foreach (EndDialogueEvent pair in endDialogueEventList)
        {
            unityEventsMap.Add(pair.id, pair.unityEvent);
        }
        isDialogueActive = false;
    }

    private void Awake()
    {

        lines = new Queue<DialogueLine>();

        continueButton = GameObject.Find("ContinueButton");
        if (continueButton != null)
        {
            continueButton.SetActive(false);
        }
    }

    public void Update()
    {
        Debug.Log("SIUREK: " + isDialogueActive);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("CHUJEC: " + isDialogueActive);
        if (isDialogueActive)
        {

            Debug.Log("Zamykam poprzedni dialog...");

            return;
            EndDialogue();
        }

        if (dialogPanel == null)
        {
            Debug.LogError("dialogPanel nie jest przypisany w DialogueManager!");
            return;
        }
        currentDialogueID = dialogue.id;
        Debug.Log($"Rozpoczynam dialog z {dialogue.dialogueLines[0].character.name}");

        Movement player = GameObject.FindWithTag("Player").GetComponent<Movement>();
        player.isMoving = false;

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
        if (dialogueLine.audioClip != null)
        {
            AudioSource audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
            audioSource.PlayOneShot(dialogueLine.audioClip);
        }
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        Movement player = GameObject.FindWithTag("Player").GetComponent<Movement>();
        player.isMoving = true;

        if (unityEventsMap.TryGetValue(currentDialogueID, out UnityEvent evt))
        {
            evt.Invoke();
        }


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