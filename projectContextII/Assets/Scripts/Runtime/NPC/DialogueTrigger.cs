using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour ,IInteractable
{
    public int dialogueIndex = 0;
    public Dialogue[] dialogue;
    public Quest quest;

    public InteractType interactType => InteractType.TALKABLE;

    public void Interact()
    {
        TriggerDialogue();

        if(quest != null) TriggerQuest();
    }

    public void TriggerDialogue()
    {
        if (dialogueIndex >= dialogue.Length) dialogueIndex = 0;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[dialogueIndex]);
    }

    public void TriggerQuest()
    {
        FindObjectOfType<QuestManager>().StartQuest(quest);
    }
}
