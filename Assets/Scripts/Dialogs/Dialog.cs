﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [TextArea(3, 10)] 
    [SerializeField] private string[] sentences;

    public string[] Sentences
    {
        get => sentences;
        set => sentences = value;
    }
}
