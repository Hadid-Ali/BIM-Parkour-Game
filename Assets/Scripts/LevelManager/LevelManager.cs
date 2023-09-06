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
    [SerializeField] private Transform LevelParent;

    private void Start()
    {

        SpawnLevel();

    }

    void SpawnLevel()
    {
        if (m_RunTestLevel)
        {
            SpawnLevel(m_LevelNumber);
        }
        else
        {
            SpawnLevel(LevelSelection.m_LevelNum);
        }
        
    }

    void SpawnLevel(int levelNumber)
    { 
        //var level = Instantiate(Resources.Load<GameObject>("Level/"+m_LevelDataScriptable.m_LevelPrefab[levelNumber].transform.name), LevelParent);
        print("Level/"+m_LevelDataScriptable.m_LevelPrefab[levelNumber].transform.name);
        var level = Resources.Load<GameObject>("Levels/"+m_LevelDataScriptable.m_LevelPrefab[levelNumber].transform.name);
        level = Instantiate(level, LevelParent);
        level.SetActive(true);
    }
    
    
    
}
