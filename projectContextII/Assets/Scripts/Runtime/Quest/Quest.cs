using System.Data;
using Unity.VisualScripting;
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

    public Quest(QuestInfoSO info, QuestState state, int currentQuestStepIndex, QuestStepState[] questStepsStates)
    {
        this.info = info;
        this.state = state;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepsStates = questStepsStates;

        //something changed durring development and the save data is now out of sync
        if(this.questStepsStates.Length != this.info.questStepPrefabs.Length)
        {
            Debug.LogWarning("Quest Step[ Prefabs and Quest Step States are "
                + "of different lengths. this indicateds something changed "
                + "with the QuestInfo and the save data now out of sync. "
                + "reset your data - as this might cause issues. QuestId: " + this.info.id
                );
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
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepsStates[currentQuestStepIndex].state);
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
