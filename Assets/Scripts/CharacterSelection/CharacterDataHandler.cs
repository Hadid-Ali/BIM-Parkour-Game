using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataHandler : MonoBehaviourSingleton<CharacterDataHandler>
{
    [SerializeField] private List<CharacterData> m_Characters;

    public CharacterData CharacterDataRetain(CharacterNames CharacterName) => m_Characters.Find(x => x.CharacterName == CharacterName);
}
