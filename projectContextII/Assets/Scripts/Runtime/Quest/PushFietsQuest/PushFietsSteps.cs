using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushFietsSteps : QuestStep
{
    private bool _fietsIsGeduwed = false;

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
        if (!_fietsIsGeduwed)
        {
            _fietsIsGeduwed=true;
        }

        if (_fietsIsGeduwed)
        {
            FinishedQuestStep();
        }
    }
}
