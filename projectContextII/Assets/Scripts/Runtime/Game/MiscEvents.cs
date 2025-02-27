using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action onFietsGeduwed;
    public void DuwFiets()
    {
        if(onFietsGeduwed != null) onFietsGeduwed();
    }
}
