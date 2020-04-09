using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (inventoryPanel.activeSelf)
            {
                DisableInventory();
            }
            else
            {
                EnableInventory();
            }
        }
    }

    public void DisableInventory()
    {
        inventoryPanel.SetActive(false);

        Time.timeScale = 1;
    }
    
    public void EnableInventory()
    {
        inventoryPanel.SetActive(true);

        Time.timeScale = 0;
    }
}
