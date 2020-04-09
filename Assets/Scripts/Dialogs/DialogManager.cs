using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;

    [SerializeField] private GameObject dialogPanel;
    
    [SerializeField] private PlayerController player;
    
    private Queue<string> sentences;

    public Queue<string> Sentences
    {
        get => sentences;
        set => sentences = value;
    }


    void Start()
    {
        sentences = new Queue<string>();
        
        dialogPanel.SetActive(false);
    }

    public void StartDialog(Dialog dialog)
    {
        dialogPanel.SetActive(true);

        player.IsInDialog = true;

        foreach (string sentence in dialog.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            sentences.Clear();

            player.IsInDialog = false;
            
            dialogPanel.SetActive(false);

            return;
        }

        string sentence = sentences.Dequeue();
        
        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;

            yield return null;
        }
    }
}
