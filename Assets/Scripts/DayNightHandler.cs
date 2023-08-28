using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightHandler : MonoBehaviour
{
    [SerializeField] private Volume m_Volume;
    [SerializeField] private Material m_Material;
    [SerializeField] private Color m_DayColor, m_NightColor;
    [SerializeField] private SpriteRenderer m_Bg;
    [SerializeField] private Sprite m_BgImgDay, m_BgImgNight;

    private void OnEnable()
    {
        GameEvents.EnvMode.Register(checkMode);
    }

    private void OnDisable()
    {
        GameEvents.EnvMode.UnRegister(checkMode);
    }

    void checkMode(bool night)
    {
        if (night)
        {
            Night();
        }
        else
        {
            Day();
        }
    }

    public void Day()
    {
        m_Volume.enabled = false;
        m_Material.color = m_DayColor;
        m_Bg.sprite = m_BgImgDay;
        RenderSettings.fog = false;
    }

    public void Night()
    {
        m_Volume.enabled = true;
        m_Material.color = m_NightColor;
        m_Bg.sprite = m_BgImgNight;
        RenderSettings.fog = true;
    } 
}
