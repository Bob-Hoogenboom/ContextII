using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour ,IInteractable
{
    public Dialogue dialogue;

    [Header("Interface")]
    [SerializeField]
    private string interactDescription = "";
    [SerializeField]
    private Sprite interactIcon;

    public InteractType interactType => InteractType.TALKABLE;
    public event IInteractable.InteractUI OnInteractUIUpdate;
    public event IInteractable.UIHideEvent OnUIHide; 
    public string InteractDescription => interactDescription;
    public Sprite InteractIcon => interactIcon;

    public void Interact()
    {
        TriggerDialogue();
    }

    public void InteractPopUp()
    {
        OnInteractUIUpdate?.Invoke(interactDescription, interactIcon, transform);
    }

    public void HidePopUp()
    {
        OnUIHide?.Invoke(); 
    }

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, null);
    }

}
