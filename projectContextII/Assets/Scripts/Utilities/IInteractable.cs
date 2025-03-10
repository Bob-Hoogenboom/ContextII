using UnityEngine;

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
    void InteractPopUp();
    void HidePopUp();

    delegate void InteractUI(string text, Sprite icon, Transform worldPos);
    delegate void UIHideEvent();

    event InteractUI OnInteractUIUpdate;
    event UIHideEvent OnUIHide;

    string InteractDescription { get; }
    Sprite InteractIcon { get; }

}
