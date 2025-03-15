using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ItemThought : MonoBehaviour
{
    public TextMeshProUGUI thoughtText; // Referencja do UI z tekstem my�li
    public float displayDuration = 3f; // Jak d�ugo wy�wietla si� my�l

    [TextArea(2, 5)]
    public List<string> thoughts = new List<string>(); // Lista my�li edytowalna w Inspektorze

    private bool isShowing = false;
    private Coroutine hideCoroutine;

    private void OnMouseDown()
    {
        if (isShowing)
        {
            HideThought(); // Je�li my�l jest wy�wietlana, klikni�cie j� ukrywa
        }
        else if (thoughts.Count > 0)
        {
            ShowThought();
        }
    }

    void ShowThought()
    {
        isShowing = true;

        // Wybierz losow� my�l z listy w Inspektorze
        thoughtText.text = thoughts[Random.Range(0, thoughts.Count)];
        thoughtText.gameObject.SetActive(true);

        // Je�li ju� dzia�a poprzednie zamykanie, zatrzymaj je
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        // Automatycznie ukryj my�l po `displayDuration`
        hideCoroutine = StartCoroutine(HideThoughtAfterDelay());
    }

    IEnumerator HideThoughtAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        HideThought();
    }

    void HideThought()
    {
        thoughtText.gameObject.SetActive(false);
        isShowing = false;
    }
}
