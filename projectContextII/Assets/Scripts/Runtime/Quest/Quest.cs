using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public bool isCompleted = false;
    public bool isActive = false;

    public string questName;
    [TextArea(3, 10)]
    public string questDescription;

    public UnityEvent endOfQuest;
}
