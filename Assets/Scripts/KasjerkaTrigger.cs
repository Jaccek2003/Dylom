using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KasjerkaTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject canvas;
    public Dialogue kasjerkaDialogue;
    public float cooldownTime = 60f; // 1 minuta cooldownu
    public float dialogueDuration = 2f; // 2 sekundy trwania dialogu

    private bool isOnCooldown = false;
    private bool playerInside = false;

    public UnityEvent Event;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.gameObject.CompareTag("Player") && !isOnCooldown)
        {
            Debug.Log("player");
            playerInside = true;
          
            StartCoroutine(ShowDialogueWithCooldown());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("exit player");
            playerInside = false;
        }
    }

    private IEnumerator ShowDialogueWithCooldown()
    {
        Debug.Log("corotine");
        while (playerInside) // Pêtla sprawdza, czy gracz nadal jest w triggerze
        {
            isOnCooldown = true;
            canvas.SetActive(true);
            dialogueManager.StartDialogue(kasjerkaDialogue);
            yield return new WaitForSeconds(dialogueDuration);
            dialogueManager.EndDialogue(); // Automatyczne zamkniêcie po 2 sek
            yield return new WaitForSeconds(cooldownTime);
            isOnCooldown = false;
        }
    }
}
