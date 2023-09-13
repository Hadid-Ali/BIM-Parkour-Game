using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public static SoundHandler Instence;
    
    [Header("Audio Source")]
    public AudioSource m_AudioSource;
    public AudioSource m_BGAudioSource;
    
    [Header("UI Audio Clips")]
    public AudioClip m_Setting;
    public AudioClip m_PlayBtn;
    public AudioClip m_SelectCharacter;
    public AudioClip m_BuyCharacter;
    public AudioClip m_StoreLeftRight;
    public AudioClip m_Store;
    public AudioClip m_Back;
    public AudioClip m_LevelSelection;
    
    [Header("GamePlay Audio Clips")]
    public AudioClip m_Coin;
    public AudioClip m_GameStart;
    public AudioClip m_GameWon;
    public AudioClip m_GameLose;
    public AudioClip m_Jump;
    public AudioClip m_Climb;
    public AudioClip m_Slide;
    public AudioClip m_Collision;
    


    private void Start()
    {
        Instence = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void playSound(AudioClip clip)
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }
}
