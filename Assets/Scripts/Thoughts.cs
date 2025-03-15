using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Thoughts : MonoBehaviour
{
    public TextMeshProUGUI Thought; // UI Text w Unity
    public float typingSpeed = 0.05f; // Szybko�� pojawiania si� liter
    public GameObject startButton; // Przycisk startu
    public float thoughtDuration = 4f; // Sta�y czas trwania ka�dej my�li

    [TextArea(3, 10)]
    public List<string> thoughts = new List<string>(); // Lista my�li edytowalna w Inspektorze

    private int currentThoughtIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private bool hasStarted = false;
    private Coroutine thoughtTimerCoroutine; // Przechowuje coroutine licz�c� czas my�li

    void Start()
    {
        Thought.text = ""; // Na start tekst pusty
    }

    public void StartThoughts()
    {
        if (!hasStarted && thoughts.Count > 0) // Sprawd�, czy s� my�li do wy�wietlenia
        {
            hasStarted = true;
            startButton.SetActive(false); // Ukryj przycisk Start
            StartCoroutine(StartAfterDelay(1f)); // Zacznij my�li po 1 sekundzie
        }
    }

    private IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartNextThought();
    }

    void Update()
    {
        if (hasStarted && Input.GetMouseButtonDown(0)) // Klikni�cie myszk�
        {
            if (isTyping)
            {
                // Je�li tekst jeszcze si� pisze, natychmiast poka� ca�o��
                StopCoroutine(typingCoroutine);
                Thought.text = thoughts[currentThoughtIndex];
                isTyping = false;
            }
            else
            {
                // Je�li tekst si� sko�czy�, ale my�l jeszcze trwa, przejd� od razu do nast�pnej
                if (thoughtTimerCoroutine != null)
                {
                    StopCoroutine(thoughtTimerCoroutine);
                }
                NextThought();
            }
        }
    }

    void StartNextThought()
    {
        if (currentThoughtIndex >= thoughts.Count)
        {
            Thought.text = ""; // Koniec my�li � ukryj tekst
            return;
        }

        Thought.text = "";
        typingCoroutine = StartCoroutine(TypeText(thoughts[currentThoughtIndex]));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        foreach (char letter in text.ToCharArray())
        {
            Thought.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;

        // Startujemy licznik czasu my�li
        thoughtTimerCoroutine = StartCoroutine(ThoughtTimer());
    }

    IEnumerator ThoughtTimer()
    {
        yield return new WaitForSeconds(thoughtDuration);
        NextThought();
    }

    void NextThought()
    {
        currentThoughtIndex++;
        StartNextThought();
    }
}
