using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public static int m_LevelNum;
    [SerializeField] private List<Button> LevelBtns;

    private void Start()
    {
        for (int i = 0; i < LevelBtns.Count; i++)
        {
            var i1 = i;
            LevelBtns[i].onClick.AddListener(() => SetLevelNum(i1));
        }
    }

    public void SetLevelNum(int num)
    {
        m_LevelNum = num;
        SceneManager.LoadScene(2);
    }
}
