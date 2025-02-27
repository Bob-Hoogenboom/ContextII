using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushFietsSteps : QuestStep
{
    private void OnEnable()
    {
        GameManager.instance.miscEvents.onFietsGeduwed += FietsGeduwed;
    }

    private void OnDisable()
    {
        GameManager.instance.miscEvents.onFietsGeduwed -= FietsGeduwed;
    }

    private void FietsGeduwed()
    {
        FinishedQuestStep();
    }
}
