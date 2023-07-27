using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "GameData/LevelData/Create")]
public class LevelDataScriptable : ScriptableObject
{
    public List<LevelData> m_LevelData;
}

[System.Serializable]
public class LevelData
{
    public int m_LevelLength;
    public LevelDifficulties m_LevelMode;
}
