using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable/LevelData/Create")]
public class LevelData : ScriptableObject
{
    public List<GameObject> m_LevelPrefab;
}
