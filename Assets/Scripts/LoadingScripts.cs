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
        if (m_PrivacyPolicyBtn != null)
        {
            m_PrivacyPolicyBtn.onClick.AddListener(PrivacyPolicy);
            m_Agree.onClick.AddListener(Agree);
        }
        
        
        if (!PlayerPrefs.HasKey("firstOpen"))
        {
            GDPRPanel.SetActive(true);
        }
        else
        {
            Init();
        }

        Application.targetFrameRate = -1;
    }

    void Agree()
    {
        PlayerprefSave.FirstOpenGame();
        Init();
    }

    void PrivacyPolicy()
    {
        Application.OpenURL("https://play.virtua.com/privacy-policy");
    }
    void Init()
    {
        if (GDPRPanel != null)
        {
            GDPRPanel.SetActive(false);
            LoadingPanel.SetActive(true);
        }
        
        GameEvents.InitFirebaseAnalytics.Raise();
        AdHandler.InitializeAds();
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        
        fillAmount.DOFillAmount(.6f, .2f);
        yield return new WaitForSeconds(.2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_LoadScene);
        yield return new WaitForSeconds(.2f);
        fillAmount.fillAmount = .9f;
        while (!asyncLoad.isDone)
        {
            fillAmount.fillAmount = 1;
            yield return null;
        }
    }
}
