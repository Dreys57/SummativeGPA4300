﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private GameObject panelMenuPause;

    void Start()
    {
        
    }
    
    void Update()
    {
        
        if (Input.GetButtonDown("Pause"))
        {
            ActivateMenuPause();
        }
    }

    public void ActivatePanelCredits()
    {
        panelCredits.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void DesactivatePanelCredits()
    {
        panelCredits.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ActivateMenuPause()
    {
        panelMenuPause.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void DesactivateMenuPause()
    {
        panelMenuPause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ActivatePanelMenu()
    {
        panelMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void DesactivatePanelMenu()
    {
        panelMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
