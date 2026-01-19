using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject dialogPanel;
    private Transform player;
    public float talkRange = 3f;
    public Dialogue dialogue;
    private bool wasFinished = false;

    public UnityEvent onDialogueStarted;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    public void ToggleDialog()
    {
        if (dialogPanel != null)
        {
            if (!wasFinished)
            {
                dialogueManager.StartDialogue(dialogue);
                wasFinished = true;
                onDialogueStarted.Invoke();
                
            }
            
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
                //if (!wasFinished)
                    dialogPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("ActiveNPC"))
        {
            ToggleDialog();
            dialogPanel.SetActive(false);
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
