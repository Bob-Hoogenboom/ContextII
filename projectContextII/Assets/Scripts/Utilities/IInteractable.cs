public enum InteractType
{
    PUSHABLE,
    TALKABLE,
    CLIMBABLE,
    DEFAULT
}

public interface IInteractable 
{
    InteractType interactType { get; }
    void Interact();
}
