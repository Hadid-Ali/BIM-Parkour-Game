using System;

public static class AdHandler
{
    public static void InitializeAds()
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.InitAds.Raise();
    }

    public static void ShowBanner()
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.ShowBannerAd.Raise();
    }

    public static void ShowRectBanner()
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.ShowRectBannerAd.Raise();
    }

    public static void HideBanner()
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.HideBannerAd.Raise();
    }

    public static void HideRectBanner()
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.HideRectBannerAd.Raise();
    }

    public static void ShowInterstitial()
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.ShowRInterstitialAd.Raise();
    }

    public static void ShowRewarded(Action reward)
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.ShowRewardedAd.Raise(reward);
    }
    public static void ShowAppOpen()
    {
        if (!SaveLoadData.m_data.NoAds)
            GameEvents.ShowAppOpenAd.Raise();
    }
}
