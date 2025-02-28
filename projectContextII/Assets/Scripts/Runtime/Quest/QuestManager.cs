using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> _questMap;

    private void Awake()
    {
        _questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameManager.instance.questEvents.onStartQuest += StartQuest;
        GameManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameManager.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameManager.instance.questEvents.onStartQuest -= StartQuest;
        GameManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        //broadcast the intitial state of all quests on startup
        foreach (Quest quest in _questMap.Values)
        {
            // initialize any loaded quest steps
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // broadcast the initial state of all quests on startup
            GameManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        //loop trough all quests
        foreach(Quest quest in _questMap.Values)
        {
            //TODO: Set an if statement for a requirement to start a quest, for now we can start every quest immediatly
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET)
            {
                ChangeQuestState(quest.data.id, QuestState.CAN_START);
            }
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        Debug.Log("Start Quest: " + id);

        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.data.id, QuestState.IN_PROGRESS);

    }

    private void AdvanceQuest(string id)
    {
        //TODO advance Quest
        Debug.Log("Advance Quest: " + id);

        Quest quest = GetQuestById(id);

        //Move to next step
        quest.MoveToNextStep();

        //if there are more steps continue
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        //if there are none, finish quest
        else
        {
            ChangeQuestState(quest.data.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ChangeQuestState(quest.data.id, QuestState.FINISHED);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestDataSO[] allQuests = Resources.LoadAll<QuestDataSO>("QuestDatas");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestDataSO questData in allQuests)
        {
            if (idToQuestMap.ContainsKey(questData.id))
            {
                Debug.LogWarning("Duplicate ID found: " + questData.id);
            }
            idToQuestMap.Add(questData.id, new Quest(questData));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = _questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in questMap: " + id);
        }
        return quest;
    }
}
