using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour ,IInteractable
{
    public Dialogue dialogue;

    public InteractType interactType => InteractType.TALKABLE;

    public void Interact()
    {
        TriggerDialogue();

    }

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
