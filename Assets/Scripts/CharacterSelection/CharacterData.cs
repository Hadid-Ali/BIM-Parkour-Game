using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData", fileName = "CharacterInfo", order = 0)]

public class CharacterData : ScriptableObject
{
    public CharacterNames CharacterName;
    public GameObject CharacterPrefab;
    public int CharacterPrice;
}
