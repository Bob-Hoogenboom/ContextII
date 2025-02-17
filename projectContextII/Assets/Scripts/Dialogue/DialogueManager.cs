using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueObject;
    public TMP_Text nameTXT;
    public Image nameBG;
    public TMP_Text dialogueTXT;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        dialogueObject.SetActive(false);
    }
 
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("StartingConvo" + dialogue.name);

        sentences.Clear();
        nameTXT.text = dialogue.name;
        nameBG.color = dialogue.color;  
        dialogueObject.SetActive(true);


        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueTXT.text = sentence;
    }

    private void EndDialogue() 
    {
        dialogueObject.SetActive(false);
        Debug.Log("convo ended");
    }
}
