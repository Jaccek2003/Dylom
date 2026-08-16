

public class ChangeDialogEvent : BaseEvent
{
    public NPC npc;
    public Dialog dialog;

    public override void Exectute()
    {
        npc.initialDialog = dialog;
        npc.InitialDialogStarted = false;
    }
}
