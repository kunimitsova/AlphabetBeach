using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoSingleton<AdsManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener {
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iOSGameId;
    [SerializeField] private bool testMode = false;
    [SerializeField] private Button _showAdButton;
    [SerializeField] private GameObject _sceneManager;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    [SerializeField] string _androidBannerID = "Banner_Android";
    string _adUnitId = null; // in case there's unsupported platforms [? why would anyway n/m]
    private string _gameId;
    public bool isAdLoaded = false;
    public bool isAdCompleted = false;

    void Awake() {
#if UNITY_IOS
_adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        InitializeAds();
    }

    private void Start() {
        LoadAd();
    }

    private void InitializeAds() {
#if UNITY_IOS
      _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
      _gameId = _androidGameId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported) {
            Advertisement.Initialize(_androidGameId, testMode, this);
        }
    }

    public void LoadAd() {  // in the example this is the equivalent of LoadRewardedAd
        //Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd() { // in the example this is the equivalent of ShowRewardedAd
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId) {
        //Debug.Log("Ad Loaded" + adUnitId);
        if (adUnitId.Equals(_adUnitId)) {
            isAdLoaded = true;
            _showAdButton.interactable = true;
            _showAdButton.GetComponent<Animator>().enabled = true;
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) {
        Debug.Log("Unity ads complete state = " + showCompletionState.ToString());
        isAdCompleted = true;
        float wait = Constants.WAIT_AFTERREWARDEDAD;
        switch (showCompletionState) {
            case UnityAdsShowCompletionState.SKIPPED:
            case UnityAdsShowCompletionState.UNKNOWN:
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                if (adUnitId == _adUnitId) {
                    _showAdButton.interactable = false;
                    _showAdButton.GetComponent<Animator>().enabled = false;
                    _showAdButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    _sceneManager.GetComponent<App_Initialize>().StartGame(wait);
                    App_Initialize.hasSeenRewardAd = true;
                }
                break;
            default:
                Debug.Log("The showAdsCompletionState was out of range");
                break;
        }
        // Time.timeScale = 1; // (we don't really need it since the game is already stopped)
        Advertisement.Banner.Show(_androidBannerID);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) {
        Debug.Log($"Error loading ad unit {adUnitId}: {error.ToString()} - {message}");
        isAdLoaded = false;
        _showAdButton.interactable = false;
        _showAdButton.GetComponent<Animator>().enabled = false;
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) {
        Debug.Log($"Error showing ad unit {adUnitId}: {error.ToString()} - {message}");
        isAdCompleted = false;
    }
    public void OnInitializationComplete() {
        //Debug.Log("Unity ads initialization complete");
        LoadBannerAd();  // this is also where we would load an interstitial ad but I hate those
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
        Debug.Log($"Unity ads initialization failed {error.ToString()} - {message}");
        isAdLoaded = false;
    }

    public void OnUnityAdsShowStart(string adUnitId) {
        //Debug.Log("OnUnityAdsShowStart ad has started...?");
        isAdLoaded = true;
        //    Time.timeScale = 0;  // (we don't really need it since the game is already stopped)
        Advertisement.Banner.Hide(); // this hides the banner when the reward ad is showing
    }

    public void OnUnityAdsShowClick(string adUnitId) {
        //Debug.Log("OnUnityAdsShowClick what does this mean");
    }

    // directly from tutorial here (tutorial is https://www.youtube.com/watch?v=7pu_CpjBFtI&t=1412s from Smart Penguins )
    public void LoadBannerAd() {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_androidBannerID,
            new BannerLoadOptions {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            }
            );
    }

    void OnBannerLoaded() {
        Advertisement.Banner.Show(_androidBannerID);
    }

    void OnBannerError(string message) {

    }
}
