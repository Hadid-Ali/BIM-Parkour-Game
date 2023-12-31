using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using Unity.Mathematics;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private Banner m_BannerAd;
    [SerializeField] private Interstitial m_InterstitialAd;
    [SerializeField] private Rewarded m_RewardedAd;
    [SerializeField] private RectBanner m_RectBannerAd;

    [SerializeField] private BannerType m_BannerType;
    [SerializeField] private BannerPosition m_BannerPosition;
    [SerializeField] private BannerPosition m_RectBannerPosition;

    private AdSize _adSize;
    private AdPosition _adPosition;
    private AdPosition _RectBannerPosition;

    private void OnEnable()
    {
        GameEvents.InitAds.Register(Init);
        GameEvents.ShowBannerAd.Register(ShowBannerAD);
        GameEvents.ShowRectBannerAd.Register(ShowRectBannerAD);
        GameEvents.HideBannerAd.Register(HideBannerAD);
        GameEvents.HideRectBannerAd.Register(HideRectBannerAD);
        GameEvents.ShowRInterstitialAd.Register(ShowInterstitialAD);
        GameEvents.ShowRewardedAd.Register(ShowRewarderVideo);
    }
    private void OnDisable()
    {
        GameEvents.InitAds.Unregister(Init);
        GameEvents.ShowBannerAd.Unregister(ShowBannerAD);
        GameEvents.HideBannerAd.Unregister(HideBannerAD);
        GameEvents.HideRectBannerAd.Unregister(HideRectBannerAD);
        GameEvents.ShowRectBannerAd.Register(ShowRectBannerAD);
        GameEvents.ShowRInterstitialAd.Unregister(ShowInterstitialAD);
        GameEvents.ShowRewardedAd.UnRegister(ShowRewarderVideo);
    }

    private void Start()
    {
        SetBannerSize();
        SetBannerPosition();
        SetRectBannerPosition();
        
        DontDestroyOnLoad(this.gameObject);
    }

    void SetBannerSize()
    {
        switch (m_BannerType)
        {
            case BannerType.Default:
                _adSize = AdSize.Banner;
                break;
            case BannerType.SmartBanner:
                _adSize = AdSize.SmartBanner;
                break;
            case BannerType.LargeBanner:
                _adSize = AdSize.IABBanner;
                break;
        }
    }
    void SetBannerPosition()
    {
        switch (m_BannerPosition)
        {
            case BannerPosition.Top:
                _adPosition = AdPosition.Top;
                break;
            case BannerPosition.Bottom:
                _adPosition = AdPosition.Bottom;
                break;
        }
    }
    
    void SetRectBannerPosition()
    {
        switch (m_RectBannerPosition)
        {
            case BannerPosition.Top:
                _RectBannerPosition = AdPosition.Top;
                break;
            case BannerPosition.Bottom:
                _RectBannerPosition = AdPosition.Bottom;
                break;
            case BannerPosition.BottomLeft:
                _RectBannerPosition = AdPosition.BottomLeft;
                break;
            case BannerPosition.BottomRight:
                _RectBannerPosition = AdPosition.BottomRight;
                break;
            case BannerPosition.TopLeft:
                _RectBannerPosition = AdPosition.TopLeft;
                break;
            case BannerPosition.TopRight:
                _RectBannerPosition = AdPosition.TopRight;
                break;
        }
    }

    void Init()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            m_InterstitialAd.LoadInterstitialAd();
            m_RewardedAd.LoadRewardedAd();
            
            Debug.Log("Ad Initialized");
        });
    }

    void ShowBannerAD()
    {
        Debug.Log("Banner Call");
        m_BannerAd.LoadAd(_adSize, _adPosition);
    }
    
    void ShowRectBannerAD()
    {
        Debug.Log("Banner Call");
        m_RectBannerAd.LoadAd(_RectBannerPosition);
    }
    
    void HideBannerAD()
    {
        m_BannerAd.DestroyAd();
    }
    
    void HideRectBannerAD()
    {
        m_RectBannerAd.DestroyAd();
    }

    void ShowInterstitialAD()
    {
        m_InterstitialAd.ShowAd();
    }

    void ShowRewarderVideo(Action Reward)
    {
        m_RewardedAd.ShowRewardedAd(Reward);
    }
    
}
