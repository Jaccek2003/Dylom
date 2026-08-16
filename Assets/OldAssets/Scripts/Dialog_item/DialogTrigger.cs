using System;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject dialogPanel;
    public Transform player;
    public float talkRange = 3f;
    public Dialogue dialogue;
    private bool wasFinished = false;

    private void Start()
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    private void ToggleDialog()
    {
        if (dialogPanel != null)
        {
            if (!wasFinished)
            {
                dialogueManager.StartDialogue(dialogue);
                wasFinished = true;
            }
            dialogPanel.SetActive(!dialogPanel.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("ActiveNPC"))
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= talkRange)
            {
                ToggleDialog();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("ActiveNPC"))
        {
            ToggleDialog();
        }
    }

    private void OnMouseDown()
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning($"Nie mo¿esz rozmawiaæ z wy³¹czon¹ postaci¹: {gameObject.name}");
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= talkRange)
        {
            Debug.Log($"Rozmowa z {gameObject.name}");
            ToggleDialog();
        }
        else
        {
            Debug.Log("Musisz byæ bli¿ej, aby porozmawiaæ.");
        }
    }
}
