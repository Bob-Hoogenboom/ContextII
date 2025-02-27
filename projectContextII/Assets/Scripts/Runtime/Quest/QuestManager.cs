using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject questUIObject;
    public TMP_Text questTitle;
    public TMP_Text questDescription;

    [Header("Quest Data")]
    private Quest quest; 

    public delegate void QuestComplete(Quest quest);
    public static event QuestComplete questComplete;


    public void StartQuest(Quest quest)
    {

    }

    
}
