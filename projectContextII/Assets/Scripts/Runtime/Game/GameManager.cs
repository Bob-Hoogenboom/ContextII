using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public GameObject fietsOBJ;
    
    public MiscEvents miscEvents;
    public QuestEvents questEvents;

    private void Awake()
    {
        if (instance != null) Destroy(this); //Mom said there can only be one GameManager in the scene QwQ
        instance = this;

        questEvents = new QuestEvents();
        miscEvents = new MiscEvents();
    }

}
