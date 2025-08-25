using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Thoughts : MonoBehaviour
{
    public TextMeshProUGUI Thought; // UI Text w Unity
    public float typingSpeed = 0.05f; // Szybkoœæ pojawiania siê liter
    public GameObject startButton; // Przycisk startu
    public float thoughtDuration = 4f; // Sta³y czas trwania ka¿dej myœli

    [TextArea(3, 10)]
    public List<string> thoughts = new List<string>(); // Lista myœli edytowalna w Inspektorze

    private int currentThoughtIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private bool hasStarted = false;
    private Coroutine thoughtTimerCoroutine; // Przechowuje coroutine licz¹c¹ czas myœli

    public static Thoughts Instance { get; private set; }

    void Awake()
    {
        // If an instance already exists and it's not this, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance
        Instance = this;

        // Optional: Keep this across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Thought.text = ""; // Na start tekst pusty
    }

    public void ResetAndStart(List<string> newThoughts)
    {
        StartCoroutine(StartThoughtsCoroutine(0.3F, newThoughts));
    }

    public void StartThoughts(List<string> newThoughts)
    {
        
        if (!hasStarted && thoughts.Count > 0) // SprawdŸ, czy s¹ myœli do wyœwietlenia
        {
            thoughts = newThoughts;
            Thought.text = ""; // Na start tekst pusty
            hasStarted = true;
            startButton.SetActive(false); // Ukryj przycisk Start
            StartCoroutine(StartAfterDelay(0.3f)); // Zacznij myœli po 1 sekundzie
        }
    }

    private IEnumerator StartThoughtsCoroutine(float delay, List<string> newThoughts)
    {
        yield return new WaitForSeconds(delay);
        StartThoughts(newThoughts);
    }

    private IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartNextThought();
    }

    void Update()
    {
        if (hasStarted && Input.GetMouseButtonDown(0)) // Klikniêcie myszk¹
        {
            if (isTyping)
            {
                // Jeœli tekst jeszcze siê pisze, natychmiast poka¿ ca³oœæ
                StopCoroutine(typingCoroutine);
                Thought.text = thoughts[currentThoughtIndex];
                isTyping = false;
            }
            else
            {
                // Jeœli tekst siê skoñczy³, ale myœl jeszcze trwa, przejdŸ od razu do nastêpnej
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
            Thought.text = ""; // Koniec myœli – ukryj tekst
            currentThoughtIndex = 0;
            hasStarted = false;
            isTyping = false;
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

        // Startujemy licznik czasu myœli
        thoughtTimerCoroutine = StartCoroutine(ThoughtTimer());
    }

    IEnumerator ThoughtTimer()
    {
        yield return new WaitForSeconds(thoughtDuration);
        NextThought();
    }

    void NextThought()
    {
        /*// Stop ongoing typing and timer coroutines
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        if (thoughtTimerCoroutine != null)
        {
            StopCoroutine(thoughtTimerCoroutine);
            thoughtTimerCoroutine = null;
        }
*/
        currentThoughtIndex++;
        StartNextThought();
    }
}
