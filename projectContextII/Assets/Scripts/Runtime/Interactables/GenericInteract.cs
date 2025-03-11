using System;
using UnityEngine;
using UnityEngine.Events;

public class GenericInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UnityEvent onInteract;

    [Header("Interface")]
    [SerializeField]
    private string interactDescription = "";
    [SerializeField]
    private Sprite interactIcon;

    public InteractType interactType => InteractType.DEFAULT;
    public event IInteractable.InteractUI OnInteractUIUpdate;
    public event IInteractable.UIHideEvent OnUIHide;
    public string InteractDescription => interactDescription;
    public Sprite InteractIcon => interactIcon;


    public void Interact()
    {
        onInteract.Invoke();
    }

    public void InteractPopUp()
    {
        OnInteractUIUpdate?.Invoke(interactDescription, interactIcon, transform);
    }

    public void HidePopUp()
    {
        OnUIHide?.Invoke();
    }
}
