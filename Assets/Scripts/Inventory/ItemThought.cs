using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ItemThought : MonoBehaviour
{
    public TextMeshProUGUI thoughtText; // Referencja do UI z tekstem myœli
    public float displayDuration = 3f; // Jak d³ugo wyœwietla siê myœl

    [TextArea(2, 5)]
    public List<string> thoughts = new List<string>(); // Lista myœli edytowalna w Inspektorze

    private bool isShowing = false;
    private Coroutine hideCoroutine;

    private void OnMouseDown()
    {
        if (isShowing)
        {
            HideThought(); // Jeœli myœl jest wyœwietlana, klikniêcie j¹ ukrywa
        }
        else if (thoughts.Count > 0)
        {
            ShowThought();
        }
    }

    void ShowThought()
    {
        isShowing = true;

        // Wybierz losow¹ myœl z listy w Inspektorze
        thoughtText.text = thoughts[Random.Range(0, thoughts.Count)];
        thoughtText.gameObject.SetActive(true);

        // Jeœli ju¿ dzia³a poprzednie zamykanie, zatrzymaj je
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        // Automatycznie ukryj myœl po `displayDuration`
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
