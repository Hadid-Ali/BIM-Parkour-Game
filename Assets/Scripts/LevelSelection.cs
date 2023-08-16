using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public static int m_LevelNum;
    [SerializeField] private TextMeshProUGUI m_CoinText;
    [SerializeField] private List<Button> LevelBtns;

    private void Start()
    {
        m_CoinText.text = SaveLoadData.m_data.Coins.ToString();
        
        for (int i = 0; i < LevelBtns.Count; i++)
        {
            var i1 = i;
            LevelBtns[i].onClick.AddListener(() => SetLevelNum(i1));

            if (i < SaveLoadData.m_data.Level)
            {
                LevelBtns[i].interactable = true;
            }
        }
    }
    
    public void SetLevelNum(int num)
    {
        m_LevelNum = num;
        StartCoroutine(LoadAsyncScene("LoadingLevel"));
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
