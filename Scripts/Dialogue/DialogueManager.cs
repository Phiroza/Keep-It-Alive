using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject talk;
    private int remaniningSentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        remaniningSentences = sentences.Count ;
        if (remaniningSentences== 0)
        {
            dialoguePanel.SetActive(false);
            talk.SetActive(true);
        }
    }

}
