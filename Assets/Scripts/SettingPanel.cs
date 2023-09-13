using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Button m_PrivacyPolicy;
    [SerializeField] private Slider Music;
    [SerializeField] private Slider Sound;
    
    void Start()
    {
        m_PrivacyPolicy.onClick.AddListener(Policy);

        SoundHandler.Instence.m_BGAudioSource.volume = SaveLoadData.m_data.Music;
        SoundHandler.Instence.m_AudioSource.volume = SaveLoadData.m_data.Sound;
        Music.value = SaveLoadData.m_data.Music;
        Sound.value = SaveLoadData.m_data.Sound;

        setMusic(SaveLoadData.m_data.Music);
        setSound(SaveLoadData.m_data.Sound);
    }

    void Policy()
    {
        Application.OpenURL("https://play.virtua.com/privacy-policy");
    }

    public void setMusic(float val)
    {
        SoundHandler.Instence.m_BGAudioSource.volume = val;
        SaveLoadData.m_data.Music = val;
        SaveLoadData.SaveData();
    }

    public void setSound(float val)
    {
        SoundHandler.Instence.m_AudioSource.volume = val;
        SaveLoadData.m_data.Sound = val;
        SaveLoadData.SaveData();
    }

}
