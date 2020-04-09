using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Dialog dialog;

    [SerializeField] private DialogManager dialogManager;

    private bool hasTriggeredDialog = false;

    public void TriggerDialog()
    {
        dialogManager.StartDialog(dialog);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasTriggeredDialog == false)
        {
            TriggerDialog();
            hasTriggeredDialog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (dialogManager.Sentences.Count == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
