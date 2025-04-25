using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue 
{
    [Tooltip("Queststate isn't used for anything other then knowing what state the dialogue is for in the inspector")]
    public QuestState QuestState;
    public string name;
    public Color color;
    public AudioClip clip;
    [TextArea(3,10)]
    public string[] sentences;

    public UnityEvent endOfDialogue;
}
