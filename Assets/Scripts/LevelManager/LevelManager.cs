using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData m_LevelDataScriptable;

    [Header("Test Level")] [SerializeField]
    private bool m_RunTestLevel;
    [SerializeField] private int m_LevelNumber;

    private void Start()
    {
        if (m_RunTestLevel)
        {
            SpawnLevel(m_LevelNumber);
            return;
        }
        
    }

    void SpawnLevel(int levelNumber)
    {
        Instantiate(m_LevelDataScriptable.m_LevelPrefab[levelNumber]);
    }
    
    
    
}
