using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public bool m_IsNightMode;

    private void OnEnable()
    {
        GameEvents.EnvMode.Raise(m_IsNightMode);
    }

    private void Start()
    {
        
    }
}
