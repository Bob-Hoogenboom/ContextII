using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    private int stepIndex;

    public void InitializeQuestStep(string id, int stepIndex)
    {
        this.questId = id;
        this.stepIndex = stepIndex;
    }


    protected void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            GameManager.instance.questEvents.AdvanceQuest(questId);

            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState)
    {
        GameManager.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState));
    }
}
