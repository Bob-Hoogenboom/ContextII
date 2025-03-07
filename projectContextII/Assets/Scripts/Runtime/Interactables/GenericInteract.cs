using UnityEngine;
using UnityEngine.Events;

public class GenericInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UnityEvent onInteract;

    public InteractType interactType => InteractType.DEFAULT;

    public void Interact()
    {
        onInteract.Invoke();
    }
}
