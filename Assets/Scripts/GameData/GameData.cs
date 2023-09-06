using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable/GameData/Create")]
public class GameData : ScriptableObject
{
    [SerializeField] private int m_Coins;
    [SerializeField] private int m_CurrentLevel;
    [SerializeField] private int m_CharactersUnlocked;
    [SerializeField] private int m_LastSelectedCharacter = 0;

    [SerializeField] private List<CharacterData> m_characterData;
    [SerializeField] private List<bool> m_characterDatabool = new();

    [SerializeField] private float m_Music, m_Sound;

    [SerializeField] private bool m_NoAds;
    [SerializeField] private bool m_UnlockAll;

    // [SerializeField] private VehicleName m_CurrentPlayerVehicleID;
    // [SerializeField] private List<PlayerVehicle> m_PlayerVehicles;

    // [SerializeField] private bool m_IsTutorialPlayed = false;

    public void ResetValue()
    {
        m_Coins = 100;
        m_CurrentLevel = 1;
    }

    public bool NoAds
    {
        get => m_NoAds;
        set => m_NoAds = value;
    }
    public bool UnlockAll
    {
        get => m_UnlockAll;
        set => m_UnlockAll = value;
    }
    
    public int Level
    {
        get => m_CurrentLevel;
        set => m_CurrentLevel = value;
    }

    public int Coins
    {
        get => m_Coins;
        set => m_Coins = value;
    }
    
    public int CharacterUnlocked
    {
        get => m_CharactersUnlocked;
        set => m_CharactersUnlocked = value;
    }
    public int LastSelectedCharacter
    {
        get => m_LastSelectedCharacter;
        set => m_LastSelectedCharacter = value;
    }
    
    public float Music
    {
        get => m_Music;
        set => m_Music = value;
    }
    
    public float Sound
    {
        get => m_Sound;
        set => m_Sound = value;
    }

    public List<CharacterData> CharacterData
    {
        get => m_characterData;
        set => m_characterData = value;
    }
    
    public List<bool> CharacterDataBool
    {
        get => m_characterDatabool;
        set => m_characterDatabool = value;
    }

    // public bool IsTutorialPlayed
    // {
    //     get => m_IsTutorialPlayed;
    //     set => m_IsTutorialPlayed = value;
    // }

    /*public VehicleName CurrentVehicle
    {
        get => m_CurrentPlayerVehicleID;
        set => m_CurrentPlayerVehicleID = value;
    }*/

    // public void MarkVehicleUnlocked(VehicleName vehicleName) => m_PlayerVehicles.Find(v => v.VehicleID == vehicleName).SetUnlocked();

    // public bool GetVehicleUnlocked(VehicleName vehicleName) => m_PlayerVehicles.Find(v => v.VehicleID == vehicleName).GetUnlocked();
}
