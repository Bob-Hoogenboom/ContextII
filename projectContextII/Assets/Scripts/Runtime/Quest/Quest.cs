using System.Data;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepsStates;

    public Quest(QuestInfoSO QuestData)
    {
        this.info = QuestData;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepsStates = new QuestStepState[info.questStepPrefabs.Length];
        for (int i = 0; i < questStepsStates.Length; i++)
        {
            questStepsStates[i] = new QuestStepState();
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            //#Can be changed to object pooling instead of instantiating and finding the component
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("The step index was out of range " + info.id + " " + currentQuestStepIndex);
        }

        return questStepPrefab;
    }
    
    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < questStepsStates.Length)
        {
            questStepsStates[stepIndex].state = questStepState.state;
        }
        else
        {
            Debug.LogWarning("StepIndex out of range: " + "quest ID = " + info.id + ". Step Index = " + stepIndex);
        }
    } 

    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepsStates);
    }
}
