using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamdomSpeedCoin : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private Vector3 m_RotationAngle;
    private void Update()
    {
        transform.Rotate(m_RotationAngle * m_Speed * Time.deltaTime);
    }
}
