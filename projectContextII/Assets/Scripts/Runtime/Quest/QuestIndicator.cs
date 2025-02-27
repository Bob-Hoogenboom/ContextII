using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIndicator : MonoBehaviour
{
    [Header("Quest Indicators")]
    [SerializeField]
    private GameObject cantStartQuest;
    [SerializeField]
    private GameObject canStartQuest;
    [SerializeField]
    private GameObject questInProgress;
    [SerializeField]
    private GameObject questCanFinished;
    [SerializeField]
    private GameObject questFinished;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        cantStartQuest.SetActive(false);
        canStartQuest.SetActive(false);
        questInProgress.SetActive(false);
        questCanFinished.SetActive(false);
        questFinished.SetActive(false);

        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) cantStartQuest.SetActive(true);
                break;
            case QuestState.CAN_START:
                if (startPoint) canStartQuest.SetActive(true);
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint) questInProgress.SetActive(true);
                break;
            case QuestState.CAN_FINISHED:
                if (finishPoint) questCanFinished.SetActive(true);
                break;
            case QuestState.FINISHED:
                if (finishPoint) questFinished.SetActive(true);
                break;
            default:
                Debug.LogWarning("Quest State not recognized for quest indicator: " + newState);
                break;
        }
    }
}
