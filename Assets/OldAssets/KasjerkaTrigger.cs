using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasjerkaTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue kasjerkaDialogue;
    public float cooldownTime = 60f; // 1 minuta cooldownu
    public float dialogueDuration = 2f; // 2 sekundy trwania dialogu

    private bool isOnCooldown = false;
    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isOnCooldown)
        {
            playerInside = true;
            StartCoroutine(ShowDialogueWithCooldown());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private IEnumerator ShowDialogueWithCooldown()
    {
        while (playerInside) // Pêtla sprawdza, czy gracz nadal jest w triggerze
        {
            isOnCooldown = true;
            dialogueManager.TryStartDialogue(kasjerkaDialogue);
            yield return new WaitForSeconds(dialogueDuration);
            dialogueManager.EndDialogue(); // Automatyczne zamkniêcie po 2 sek
            yield return new WaitForSeconds(cooldownTime);
            isOnCooldown = false;
        }
    }
}
