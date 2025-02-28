using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;

    public void InitializeQuestStep(string id)
    {
        this.questId = id;
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
}
