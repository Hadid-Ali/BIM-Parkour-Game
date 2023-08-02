using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private List<GameObject> m_Characters = new List<GameObject>();
    private List<CharacterData> m_Characters1 = new List<CharacterData>();
    [SerializeField] private List<CharacterNames> m_CharactersName;
    private int CurrentCharacter = 0;

    public static string m_SelectedCharacter;

    private void OnEnable()
    {
        GameEvents.SelectedCharacter.Register(SelectedCharacter);
    }

    private void OnDisable()
    {
        GameEvents.SelectedCharacter.Unregister(SelectedCharacter);
    }

    private void Start()
    {
        GetCharacterData();
    }

    void GetCharacterData()
    {
        for (int i = 0; i < m_CharactersName.Count; i++)
        {
            m_Characters1.Add(CharacterDataHandler.Instance.CharacterDataRetain(m_CharactersName[i]));
            SpawnCharacter(i);
        }
        
        m_Characters[CurrentCharacter].SetActive(true);
    }

    void SpawnCharacter(int i)
    {
        m_Characters.Add(Instantiate(m_Characters1[i].CharacterPrefab, transform));
    }

    public void LeftBtnPress()
    {
        CurrentCharacter--;
        UpdateCharater(CurrentCharacter);
    }
    public void RightBtnPress()
    {
        CurrentCharacter++;
        UpdateCharater(CurrentCharacter);
    }

    void UpdateCharater(int CharacterIndex)
    {
        if (CharacterIndex >= m_CharactersName.Count)
        {
            CurrentCharacter = 0;
        }
        else if(CharacterIndex <= -1)
        {
            CurrentCharacter = m_CharactersName.Count - 1;
        }
        
        for (int i = 0; i < m_Characters.Count; i++)
        {
            m_Characters[i].SetActive(false);
        }
        
        m_Characters[CurrentCharacter].SetActive(true);
    }

    private void SelectedCharacter()
    {
        m_SelectedCharacter = m_CharactersName[CurrentCharacter].ToString();
    }

}
