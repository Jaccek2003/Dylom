

public class ThoughtEvent : BaseEvent
{
    public Thought thought;

    public override void Exectute()
    {
        FindObjectOfType<ThoughtManager>().StartThought(thought);
    }
}
