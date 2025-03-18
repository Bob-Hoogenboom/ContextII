using Dreamteck;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// This is a script where you can place various events for quests or other game evenmts
/// </summary>
public class MiscEvents
{
    //Quest: Duw Fiets om Test Quest
    //TODO remove quest
    public event Action onFietsGeduwed;

    //Quest: Bloemetje Brengen
    public event Action OnFlowersCollected;
    public void FlowerGet()
    {
        if (OnFlowersCollected != null)
        {
            OnFlowersCollected();
        }
    }

} 
