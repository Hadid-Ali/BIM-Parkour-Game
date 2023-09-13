using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadData : MonoBehaviour
{
    [SerializeField] private GameData m_GameData;
    public static GameData m_data;
    private void Start()
    {
        
        LoadData();
        m_data = m_GameData;
        GetCharacterdata();
        DontDestroyOnLoad(this.gameObject);
    }

    static void SetCharacterdata()
    {
        for (int i = 0; i < m_data.CharacterData.Count; i++)
        {
            m_data.CharacterDataBool[i] = m_data.CharacterData[i].Locked;
        }
    }

    void GetCharacterdata()
    {
        if (PlayerPrefs.HasKey("Save"))
            for (int i = 0; i < m_data.CharacterData.Count; i++)
            {
                m_data.CharacterData[i].Locked = m_data.CharacterDataBool[i];
            }
    }

    public static void SaveData()
    {
        SetCharacterdata();
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
