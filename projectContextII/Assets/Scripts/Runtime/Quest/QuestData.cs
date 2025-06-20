using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int questStepIndex;
    public QuestStepState[] questStepState;

    public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepState)
    {
        this.state = state;
        this.questStepIndex = questStepIndex;
        this.questStepState = questStepState;
    }   
}
