

public class DialogEvent : BaseEvent
{
    public Dialog dialog;

    public override void Exectute()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
