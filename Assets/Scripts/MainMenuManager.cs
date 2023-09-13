using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI m_CoinText;
    [SerializeField] private Button m_PlayBtn;
    [SerializeField] private Button m_Store;
    [SerializeField] private Button m_Setting;
    [SerializeField] private Button m_NoADs;
    [SerializeField] private Button m_UnlockAll;
    
    [SerializeField] private GameObject StorePanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject MenuPanel;

    private void OnEnable()
    {
        GameEvents.NoAds.Register(NoADs);
        GameEvents.UnlockAll.Register(UnlockAll);
    }

    private void OnDisable()
    {
        GameEvents.NoAds.Unregister(NoADs);
        GameEvents.UnlockAll.Unregister(UnlockAll);
    }

    void Start()
    {
        SoundHandler.Instence.m_BGAudioSource.Play();
        AdHandler.ShowBanner();
        m_CoinText.text = SaveLoadData.m_data.Coins.ToString();
        m_PlayBtn.onClick.AddListener(PlayBtn);
        m_Store.onClick.AddListener(Store);
        m_Setting.onClick.AddListener(Setting);

        if (SaveLoadData.m_data.NoAds)
        {
            m_NoADs.gameObject.SetActive(false);
        }
        if (SaveLoadData.m_data.UnlockAll)
        {
            m_UnlockAll.gameObject.SetActive(false);
        }

        
    }

    void PlayBtn()
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_PlayBtn);

        StartCoroutine(LoadAsyncScene("LevelSelection"));
    }

    void Store()
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_Store);
    }

    void Setting()
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_Setting);
    }

    void NoADs()
    {
        
        m_NoADs.gameObject.SetActive(false);
        AdHandler.HideBanner();
        SaveLoadData.m_data.NoAds = true;
        m_UnlockAll.transform.position = m_NoADs.transform.position;
        SaveLoadData.SaveData();
    }
    
    void UnlockAll()
    {
        NoADs();
        m_UnlockAll.gameObject.SetActive(false);
        SaveLoadData.m_data.UnlockAll = true;
        SaveLoadData.m_data.Level = 30;
        for (int i = 0; i < SaveLoadData.m_data.CharacterData.Count; i++)
        {
            //print(SaveLoadData.m_data.CharacterDataBool[i].ToString());
            SaveLoadData.m_data.CharacterData[i].Locked = false;
            //print(SaveLoadData.m_data.CharacterDataBool[i].ToString());
        }
        SaveLoadData.SaveData();
    }

    IEnumerator LoadAsyncScene(string m_LoadScene)
    {
        yield return new WaitForSeconds(.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_LoadScene);
        yield return new WaitForSeconds(.5f);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
