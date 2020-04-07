using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;

    private Queue<string> sentences;

    [SerializeField] private GameObject dialogPanel;

    void Start()
    {
        sentences = new Queue<string>();
        
        dialogPanel.SetActive(false);
    }

    public void StartDialog(Dialog dialog)
    {
        dialogPanel.SetActive(true);

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
