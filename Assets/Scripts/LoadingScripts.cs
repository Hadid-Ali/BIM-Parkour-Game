﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LoadingScripts : MonoBehaviour
{
    [SerializeField] Image fillAmount;
    [SerializeField] private string m_LoadScene;

    [SerializeField] private Button m_PrivacyPolicyBtn;
    [SerializeField] private Button m_Agree;

    [SerializeField] private GameObject GDPRPanel, LoadingPanel;
    void Start()
    {
        if (!PlayerPrefs.HasKey("firstOpen"))
        {
            GDPRPanel.SetActive(true);
        }
        else
        {
            
        }
        
        
    }

    void Init()
    {
        StartCoroutine(LoadYourAsyncScene());
        PlayerprefSave.FirstOpenGame();
        
        AdHandler.InitializeAds();
    }

    IEnumerator LoadYourAsyncScene()
    {
        fillAmount.DOFillAmount(.4f, .4f);
        yield return new WaitForSeconds(.4f);
        fillAmount.DOFillAmount(.6f, .5f);
        yield return new WaitForSeconds(.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_LoadScene);
        yield return new WaitForSeconds(.5f);
        fillAmount.fillAmount = .9f;
        while (!asyncLoad.isDone)
        {
            fillAmount.fillAmount = 1;
            yield return null;
        }
    }
}
