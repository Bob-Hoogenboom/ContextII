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
            GameManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        //loop trough all quests
        foreach(Quest quest in _questMap.Values)
        {
            //TODO: Set an if statement for a requirement to start a quest, for now we can start every quest immediatly
            ChangeQuestState(quest.data.id, QuestState.CAN_START);
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
        //TODO start Quest
        Debug.Log("Start Quest: " + id);
    }

    private void AdvanceQuest(string id)
    {
        //TODO advance Quest
        Debug.Log("Advance Quest: " + id);
    }

    private void FinishQuest(string id)
    {
        //TODO finish Quest
        Debug.Log("Finish Quest: " + id);
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
