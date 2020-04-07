using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Dialog dialog;

    [SerializeField] private DialogManager dialogManager;

    public void TriggerDialog()
    {
        dialogManager.StartDialog(dialog);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialog();
        }
    }
}
