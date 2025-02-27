using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            //TODO - Advance to next step in the quest

            Destroy(this.gameObject);
        }
    }
}
