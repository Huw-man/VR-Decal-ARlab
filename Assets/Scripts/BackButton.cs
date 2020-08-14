﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;


public class BackButton : MonoBehaviour
{
    [SerializeField]
    GameObject m_BackButton;
    public GameObject backButton
    {
        get { return m_BackButton; }
        set { m_BackButton = value; }
    }

    void Start()
    {
        if (Application.CanStreamedLevelBeLoaded("MainMenu"))
        {
            m_BackButton.SetActive(true);
        }
    }

    public void BackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}

