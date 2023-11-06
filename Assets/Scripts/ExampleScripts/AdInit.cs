//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Advertisements;

//public class AdInit : MonoBehaviour, IUnityAdsInitializationListener {
//    [SerializeField] string _androidGameId;
//    [SerializeField] string _iOSGameId;
//    [SerializeField] bool testMode = true;
//    private string _gameId;
    
//    void Awake() {
//        InitializeAds();
//    }

//    public void InitializeAds() {
//#if UNITY_IOS
//      _gameId = _iOSGameId;
//#elif UNITY_ANDROID
//      _gameId = _androidGameId;
//#elif UNITY_EDITOR
//      _gameId = _androidGameId;
//#endif
//        if (!Advertisement.isInitialized && Advertisement.isSupported) {
//            Advertisement.Initialize(_gameId, testMode, this);
//        }
//    }

//    public void OnInitializationComplete() {
//        Debug.Log("Unity ads initialization complete");
//    }

//    public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
//        Debug.Log($"Unity ads initialization failed {error.ToString()} - {message}");
//    }

//}
