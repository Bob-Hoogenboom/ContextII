using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public GameObject fietsOBJ;
    
    public MiscEvents miscEvents;
    public QuestEvents questEvents;

    private void Awake()
    {
        //Mom said there can only be one GameManager in the scene QwQ
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        instance = this;

        questEvents = new QuestEvents();
        miscEvents = new MiscEvents();
    }
}
