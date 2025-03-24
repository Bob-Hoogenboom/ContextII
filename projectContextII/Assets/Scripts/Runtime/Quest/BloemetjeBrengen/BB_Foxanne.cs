using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BB_Foxanne : QuestStep
{
    private bool _bloemetjeGebracht = false;

    public void BloemetjeGeven()
    {
        if (_bloemetjeGebracht) return;

        UpdateState();

        FinishedQuestStep();

        _bloemetjeGebracht = true;
    }

    private void UpdateState()
    {
        string state = _bloemetjeGebracht.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this._bloemetjeGebracht = System.Boolean.Parse(state);
        UpdateState();
    }
}
