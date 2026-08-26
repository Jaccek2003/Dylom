using UnityEngine;

public class DialogTriggerEnter : MonoBehaviour
{
    [SerializeField] private float cooldown = 60f;
    [SerializeField] private Dialog dialog;
    private DialogManager dialogManager;

    private bool onCooldown = false;

    private void Start()
    {
        dialogManager  = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (onCooldown || !other.CompareTag("Player") || dialogManager.CurrentDialog != null)
            return;

        dialogManager.StartDialog(dialog);

        onCooldown = true;
        Invoke(nameof(ResetCooldown), cooldown);
    }

    private void ResetCooldown()
    {
        onCooldown = false;
    }
}
