using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private bool[] isFull;

    [SerializeField] private GameObject[] slots;

    public bool[] IsFull
    {
        get => isFull;
        set => isFull = value;
    }

    public GameObject[] Slots
    {
        get => slots;
        set => slots = value;
    }
}
