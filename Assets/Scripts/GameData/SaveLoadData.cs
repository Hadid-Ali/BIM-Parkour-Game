using System;
using System.Collections;
using System.Collections.Generic;
using IronSourceJSON;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    [SerializeField] private GameData m_GameData;
    public static GameData m_data;
    private void Start()
    {
        
        LoadData();
        m_data = m_GameData;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void SaveData()
    {
        string Save = JsonUtility.ToJson(m_data);
        PlayerPrefs.SetString("Save", Save);
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            string Load = PlayerPrefs.GetString("Save");
            JsonUtility.FromJsonOverwrite(Load, m_GameData);
        }
        else
        {
            m_GameData.ResetValue();
        }
    }
}
