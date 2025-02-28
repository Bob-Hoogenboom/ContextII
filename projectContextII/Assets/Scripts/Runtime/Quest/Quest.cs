using System.Data;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public QuestDataSO data;
    public QuestState state;
    private int currentQuestStepIndex;

    public Quest(QuestDataSO QuestData)
    {
        this.data = QuestData;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < data.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            //#Can be changed to object pooling instead of instantiating and finding the component
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(data.id);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = data.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("The step index was out of range " + data.id + " " + currentQuestStepIndex);
        }

        return questStepPrefab;
    }
}
