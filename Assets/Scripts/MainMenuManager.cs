using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI m_CoinText;
    [SerializeField] private Button m_PlayBtn;
    [SerializeField] private Button m_Store;
    [SerializeField] private Button m_Setting;

    void Start()
    {
        AdHandler.ShowBanner();
        m_CoinText.text = SaveLoadData.m_data.Coins.ToString();
        m_PlayBtn.onClick.AddListener(PlayBtn);
    }

    void PlayBtn()
    {
        StartCoroutine(LoadAsyncScene("LevelSelection"));
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
