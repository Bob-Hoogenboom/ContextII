using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDataSO", menuName = "ScriptableObjects/QuestDataSO", order = 1)]
public class QuestDataSO : ScriptableObject
{
    [field: SerializeField]
    public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public int respectRequirement;
    public QuestDataSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int duurzaamheid;

    //enforce the name of the ID to be the name of the object
    private void OnValidate()
    {
        #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
