using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableTest : MonoBehaviour, IInteractable
{
    public InteractType interactType => InteractType.TALKABLE;
}
