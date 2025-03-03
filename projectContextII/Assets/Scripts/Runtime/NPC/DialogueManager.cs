using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    public GameObject dialogueUI;
    public TMP_Text nameTXT;
    public Image nameBG;
    public TMP_Text dialogueTXT;
    public AudioSource dialogueAudioSource;

    private Player.Player _player;
    private Dialogue _dialogue;

    [Header("Variables")]
    [SerializeField] private float dialogueSpeed = 0.05f;
    private Queue<string> sentences;


    private void Start()
    {
        _player = FindObjectOfType<Player.Player>();
        sentences = new Queue<string>();
        dialogueUI.SetActive(false);
    }
 
    public void StartDialogue(Dialogue dialogue)
    {
        _dialogue = dialogue;
        Debug.Log("StartingConvo" + _dialogue.name);

        //StartDialogueAudio();
        sentences.Clear();
        nameTXT.text = _dialogue.name;
        nameBG.color = _dialogue.color;  
        dialogueUI.SetActive(true);


        foreach (string sentence in _dialogue.sentences)
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
        _player.isInteracting = false;//Talking is done!
        dialogueUI.SetActive(false);

        _dialogue.endOfDialogue.Invoke();
        Debug.Log("convo ended");
    }

    private void StartDialogueAudio()
    {
        dialogueAudioSource.clip = _dialogue.clip;
        dialogueAudioSource.Play();
    }
}
