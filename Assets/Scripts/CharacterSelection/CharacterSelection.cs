using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [Header("UI Buttons")] 
    [SerializeField] private Button m_Left;
    [SerializeField] private Button m_Right;
    [SerializeField] private Button m_Buy;
    [SerializeField] private Button m_Select;

    [Header("UI Text")] 
    [SerializeField] private TextMeshProUGUI BuyPrice;
    [SerializeField] private TextMeshProUGUI SelectBtnText;
    
    
    private List<GameObject> m_Characters = new List<GameObject>();
    private List<CharacterData> m_Characters1 = new List<CharacterData>();
    [Header("Character data")] 
    [SerializeField] private List<CharacterNames> m_CharactersName;
    private int CurrentCharacter = 0;

    public static bool firsttime = false;
    
    private void OnEnable()
    {
        if (firsttime)
        {
            CurrentCharacter = SaveLoadData.m_data.LastSelectedCharacter;
            UpdateCharater(SaveLoadData.m_data.LastSelectedCharacter);
        }
        
    }
    
    private void Start()
    {
        firsttime = true;
        CurrentCharacter = SaveLoadData.m_data.LastSelectedCharacter;
        m_Left.onClick.AddListener(LeftBtnPress);
        m_Right.onClick.AddListener(RightBtnPress);
        m_Buy.onClick.AddListener(BuyCharacter);
        m_Select.onClick.AddListener(SelectCharacter);
        GetCharacterData();
    }

    void GetCharacterData()
    {
        for (int i = 0; i < m_CharactersName.Count; i++)
        {
            m_Characters1.Add(CharacterDataHandler.Instance.CharacterDataRetain(m_CharactersName[i]));
            SpawnCharacter(i);
        }
        
        UpdateCharater(SaveLoadData.m_data.LastSelectedCharacter);
    }

    void SpawnCharacter(int i)
    {
        m_Characters.Add(Instantiate(m_Characters1[i].CharacterPrefab));
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

        if (m_Characters1[CurrentCharacter].Locked)
        {
            m_Buy.gameObject.SetActive(true);
            m_Select.gameObject.SetActive(false);
            BuyPrice.text = m_Characters1[CurrentCharacter].CharacterPrice.ToString();
        }
        else
        {
            m_Buy.gameObject.SetActive(false);
            m_Select.gameObject.SetActive(true);
            if (SaveLoadData.m_data.LastSelectedCharacter == CurrentCharacter)
            {
                SelectBtnText.text = "SELECTED";
                m_Select.interactable = false;
            }
            else
            {
                m_Select.interactable = true;
                SelectBtnText.text = "SELECT";
            }
        }
    }

    void BuyCharacter()
    {
        if (SaveLoadData.m_data.Coins >= m_Characters1[CurrentCharacter].CharacterPrice )
        {
            m_Characters1[CurrentCharacter].Locked = false;
            SaveLoadData.SaveData();
            UpdateCharater(CurrentCharacter);
        }
    }

    void SelectCharacter()
    {
        SaveLoadData.m_data.LastSelectedCharacter = CurrentCharacter;
        SaveLoadData.SaveData();
        UpdateCharater(CurrentCharacter);
    }
    

}
