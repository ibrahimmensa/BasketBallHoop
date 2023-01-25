using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public enum RewardedAdType
{
    FREECOINS,
    REVIVE,
    FREECHARACTER,
    DOUBLEREWARD
}

public class UnityAdsManager : Singleton<UnityAdsManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    bool rewardAds = false;
    public int num;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    [SerializeField] string _androidAdUnitIdInterstitial = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitIdInterstitial = "Interstitial_iOS";
    public RewardedAdType currentAdType;
    string _adUnitId = null; // This will remain null for unsupported platforms
    string _adUnitIdInterstitial = null;
    GameObject currentBallObject;
    void Awake()
    {
            InitializeAds();
    }
    
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        _adUnitIdInterstitial = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitIdInterstitial
            : _androidAdUnitIdInterstitial;
    }

    public void TRUEBOOL()
    {
        rewardAds = true;
    }
    #region RewardedAd
    public void OnInitializationComplete()
    {
        if (PlayerPrefs.GetInt("NoAds",0)==0)
        {
            Debug.Log("Unity Ads initialization complete.");
            LoadAd();
           // LoadAdInterstitial();
            if (rewardAds == true)
            {
                rewardAds = false;
               ShowAdInterstitial();
                ShowAd(currentAdType);
                
            }
        }
       
    }
   
    //public void BuyAds()
    //{
    //    if (AdsNum.instance.num ==0)
    //    {
    //        BuyBall.instance.BuyWithAds();
    //        Debug.Log("Mohsin");
    //    }
    //}
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }


    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            Debug.Log("Rewarded Ad loaded");
            // Configure the button to call the ShowAd() method when clicked:
            //_showAdButton.onClick.AddListener(ShowAd);
            //// Enable the button for users to click:
            //_showAdButton.interactable = true;
        }
        if (adUnitId.Equals(_adUnitIdInterstitial))
        {
            Debug.Log("Interstitial Loaded");
        }
    }
    public void GetFreeCoins()
    {
        ShowAd(RewardedAdType.FREECOINS);
        Debug.Log("Reward Testing Button CLicked");
    }
    public void GetFreeBall(GameObject obj)
    {
        currentBallObject = obj;
        ShowAd(RewardedAdType.FREECHARACTER);
        //Debug.Log("Reward Testing Button CLicked");
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd(RewardedAdType type)
    {
        Debug.Log("Showing Ad");
        currentAdType = type;
        if (currentAdType == RewardedAdType.FREECOINS)
        {
            Debug.Log("Reward Testing Button SHowad called");
        }
        // Disable the button:
        //_showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            if(currentAdType == RewardedAdType.FREECOINS)
            {
                Debug.Log("Reward Testing Button reward completed");
                MainMenu.instance.amount = MainMenu.instance.amount + 50;
                PlayerPrefs.SetInt("Coins", MainMenu.instance.amount);
            }
            if (currentAdType == RewardedAdType.FREECHARACTER)
            {
                Debug.Log("entered free ball");
                currentBallObject.GetComponent<BuyBall>().AdCompleted();
            }
            // Grant a reward.

            // Load another ad:
            Advertisement.Load(_adUnitId, this);
            //switch (currentAdType)
            //{
            //    case RewardedAdType.FREECOINS:
            //        int totalCoins = PlayerPrefs.GetInt("Coins", 0);
            //        totalCoins = totalCoins + 50;
            //        PlayerPrefs.SetInt("Coins", totalCoins);
            //        MenuHandler.Instance.mainMenu.updateStats();
            //        break;
            //    case RewardedAdType.REVIVE:
            //        SceneHandler.Instance.revivePlayer();
            //        break;
            //    case RewardedAdType.DOUBLEREWARD:
            //        MenuHandler.Instance.levelCompleteHandler.doubleRewardResponse();
            //        break;
            //}
        }
        else
        {
            LoadAdInterstitial();
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        LoadAd();
        LoadAdInterstitial();
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        LoadAd();
        LoadAdInterstitial();
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    #endregion


    #region InterstitialAd
    public void LoadAdInterstitial()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitIdInterstitial);
        Advertisement.Load(_adUnitIdInterstitial, this);
    }

    public void ShowAdInterstitial()
    {
        if (PlayerPrefs.GetInt("NoAds") == 0)
        {
            Debug.Log("Showing Ad: " + _adUnitIdInterstitial);
            Advertisement.Show(_adUnitIdInterstitial, this);

        }
        // Note that if the ad content wasn't previously loaded, this method will fail
        
    }

    // Implement Load Listener and Show Listener interface methods: 

    #endregion
}
