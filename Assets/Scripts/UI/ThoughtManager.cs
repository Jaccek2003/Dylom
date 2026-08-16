using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Thought
{
    public string id;
    public List<ThoughtLine> thoughtLines = new List<ThoughtLine>();
    public UnityEvent onThoughtStarted;
    public UnityEvent onThoughtEnded;
}

[System.Serializable]
public class ThoughtLine
{
    [TextArea(3, 10)]
    public string line;

}

public class ThoughtManager : MonoBehaviour
{
    public Canvas thoughtCanvas;

    public float typingSpeed = 0.2f;

    public float waitBetweenThoughts = 1.0f;

    private Queue<ThoughtLine> lines;

    private Thought currentThought;

    public Thought CurrentThought
    {
        get => currentThought;
    }

    private void Start()
    {
        lines = new Queue<ThoughtLine>();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    DisplayNextDialogueLine();
        //}
    }

    public void StartThought(Thought thought)
    {
        currentThought = thought;
        thoughtCanvas.gameObject.SetActive(true);

        StopAllCoroutines();
        lines.Clear();
        if (currentThought != null)
            currentThought.onThoughtStarted.Invoke();

        foreach (ThoughtLine thoughtLine in currentThought.thoughtLines)
        {
            lines.Enqueue(thoughtLine);
        }

        DisplayNextDialogueLine();

    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndThought();
            return;
        }

        ThoughtLine currentLine = lines.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(ThoughtLine thoughtLine)
    {
        GetTextArea().text = "";
        foreach (char letter in thoughtLine.line.ToCharArray())
        {
            GetTextArea().text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }


        yield return new WaitForSeconds(waitBetweenThoughts);

        DisplayNextDialogueLine();
    }

    public void EndThought()
    {

        //if (unityEventsMap.TryGetValue(currentDialogueID, out UnityEvent evt))
        //{
        //    evt.Invoke();
        //}

        currentThought.onThoughtEnded.Invoke();
        currentThought = null;
        thoughtCanvas.gameObject.SetActive(false);
    }


    private TextMeshProUGUI GetTextArea()
    {
        return thoughtCanvas.transform.Find("Text area").GetComponent<TextMeshProUGUI>();
    }
}
