using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedHandler : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private void OnEnable()
    {
        GameEvents.PlayerSpeed.Register(SetPlayerSpeed);
        GameEvents.PlayerAnimSpeed.Register(SetPlayerAnimSpeed);
    }

    private void OnDisable()
    {
        GameEvents.PlayerSpeed.UnRegister(SetPlayerSpeed);
        GameEvents.PlayerAnimSpeed.UnRegister(SetPlayerAnimSpeed);
    }

    void SetPlayerSpeed(float speed)
    {
        player.speed = speed;
        player.defaultspeed = speed;
    }

    void SetPlayerAnimSpeed(float speed)
    {
        player.anim.speed = speed;
    }
}
