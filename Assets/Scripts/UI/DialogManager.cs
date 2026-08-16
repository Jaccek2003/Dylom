using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialog
{
    public string id;
    public List<DialogLine> dialogLines = new List<DialogLine>();
    public UnityEvent onDialogStarted;
    public UnityEvent onDialogEnded;
}

[System.Serializable]
public class DialogLine
{
    public DialogCharacter character;
    [TextArea(3, 10)]
    public string line;
    public AudioClip audioClip;
    public UnityEvent onDialogLineStarted;
}

[System.Serializable]
public class DialogCharacter
{
    public string name;
    public Sprite icon;
}

public class DialogManager : MonoBehaviour
{
    private PlayerMovement player;
    public Canvas dialogCanvas;

    public float typingSpeed = 0.2f;

    private Queue<DialogLine> lines;

    private Dialog currentDialog;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
        //dialogCanvas = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<Canvas>();

        lines = new Queue<DialogLine>();

        GetContinueButton().onClick.AddListener(OnContinueButtonClicked);
    }

    public void StartDialog(Dialog dialog)
    {
        currentDialog = dialog;
        player.MovementEnabled = false;
        dialogCanvas.gameObject.SetActive(true);

        StopAllCoroutines();
        lines.Clear();
        if (currentDialog != null)
            currentDialog.onDialogStarted.Invoke();

        foreach (DialogLine dialogLine in currentDialog.dialogLines)
        {
            lines.Enqueue(dialogLine);
        }

        currentDialog.onDialogStarted.Invoke();
        DisplayNextDialogueLine();

    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialog();
            return;
        }

        DialogLine currentLine = lines.Dequeue();
        currentLine.onDialogLineStarted.Invoke();
        Image characterIcon = GetCharacterIcon();
        characterIcon.sprite = currentLine.character.icon;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogLine dialogLine)
    {
        if (dialogLine.audioClip != null)
        {
            AudioSource audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
            audioSource.PlayOneShot(dialogLine.audioClip);
        }
        GetTextArea().text = "";
        foreach (char letter in dialogLine.line.ToCharArray())
        {
            GetTextArea().text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialog()
    {
        player.MovementEnabled = true;

        //if (unityEventsMap.TryGetValue(currentDialogueID, out UnityEvent evt))
        //{
        //    evt.Invoke();
        //}

        currentDialog.onDialogEnded.Invoke();
        currentDialog = null;
        dialogCanvas.gameObject.SetActive(false);
    }

    private Image GetCharacterIcon()
    {
        return dialogCanvas.transform.Find("Character icon").GetComponent<Image>();
    }

    private TextMeshProUGUI GetTextArea()
    {
        return dialogCanvas.transform.Find("Text area").GetComponent<TextMeshProUGUI>();
    }
    private Button GetContinueButton()
    {
        return dialogCanvas.transform.Find("Continue button").GetComponent<Button>();
    }

    private void OnContinueButtonClicked()
    {
        DisplayNextDialogueLine();
    }

    private void OnDestroy()
    {
        if(dialogCanvas != null)
            GetContinueButton().onClick.RemoveListener(OnContinueButtonClicked);
    }

    private void OnDisable()
    {
        if (dialogCanvas != null)
            GetContinueButton().onClick.RemoveListener(OnContinueButtonClicked);
    }
}
