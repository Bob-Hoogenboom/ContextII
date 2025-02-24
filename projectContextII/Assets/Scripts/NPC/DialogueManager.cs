using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueObject;
    public TMP_Text nameTXT;
    public Image nameBG;
    public TMP_Text dialogueTXT;

    [SerializeField] private float dialogueSpeed = 0.05f;
    private Player.Player _player;

    private Queue<string> sentences;

    private void Start()
    {
        _player = FindObjectOfType<Player.Player>();
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
        StopAllCoroutines(); //To avoid coroutines running simultaneously
        StartCoroutine(DialogueAnimation(sentence));
    }

    IEnumerator DialogueAnimation(string sentence)
    {
        dialogueTXT.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueTXT.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    private void EndDialogue() 
    {
        _player.isInteracting = false;          //Talking is done!
        dialogueObject.SetActive(false);
        Debug.Log("convo ended");
    }
}
