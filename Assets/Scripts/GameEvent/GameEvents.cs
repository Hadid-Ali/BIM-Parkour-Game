using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameEvents
{
    public static GameEvent SelectedCharacter = new();
    public static GameEvent<float> PlayerSpeed = new();
    public static GameEvent<float> PlayerAnimSpeed = new();
    public static GameEvent<bool> EnvMode = new();

    public static GameEvent InitAds = new();
    public static GameEvent InitFirebaseAnalytics = new();
    public static GameEvent ShowBannerAd = new();
    public static GameEvent ShowRectBannerAd = new();
    public static GameEvent HideBannerAd = new();
    public static GameEvent HideRectBannerAd = new();
    public static GameEvent ShowRInterstitialAd = new();
    public static GameEvent<Action> ShowRewardedAd = new();
    
    public static GameEvent NoAds = new();
    public static GameEvent UnlockAll = new();

}
