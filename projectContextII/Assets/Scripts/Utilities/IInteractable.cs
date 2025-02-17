public enum InteractType
{
    PUSHABLE,
    TALKABLE,
    CLIMBABLE
}

public interface IInteractable 
{
    InteractType interactType { get; }
    void Interact();
}
