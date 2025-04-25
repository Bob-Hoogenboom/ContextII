using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    public int dialogueIndex = 0;
    public Dialogue[] dialogue;
    public CinemachineVirtualCamera dialogueCamera;

    [Header("Quest")]
    [SerializeField]
    private QuestInfoSO questData;
    private QuestState _currentQuestState;

    [Header("Config")]
    [SerializeField]
    private bool startPoint = true;
    [SerializeField]
    private bool finishPoint = true;

    [SerializeField]
    private QuestIndicator _questIndicator;


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


    private void Awake()
    {
        _questIndicator.GetComponentInChildren<QuestIndicator>();
    }

    private void OnEnable()
    {
        GameManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }



    public void Interact()
    {
        TriggerDialogue();

        if (questData != null) TriggerQuest();
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
        //Ternary operator 'var = con? exp true : exp false;'
        int dialogueToPlay = dialogueIndex >= dialogue.Length? dialogueIndex -1 : dialogueIndex;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[dialogueToPlay], dialogueCamera);
    }

    private void TriggerQuest()
    {
        if(_currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameManager.instance.questEvents.StartQuest(questData.id);
        }
        else if (_currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameManager.instance.questEvents.FinishQuest(questData.id);
        }

    }

    private void QuestStateChange(Quest quest)
    {
        //Only update the quest stat if the point has the corresponding quest 
        if (questData == null) return;

        if (quest.info.id.Equals(questData.id))
        {
            _currentQuestState = quest.state;
            //Debug.Log("Quest with id: " + questData.id + " updated to State: " + _currentQuestState);
            _questIndicator.SetState(_currentQuestState, startPoint, finishPoint);
            dialogueIndex = ((int)_currentQuestState);
        }
    }
}
