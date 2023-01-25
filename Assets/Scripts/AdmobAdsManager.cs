using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.IO;

public class AdmobAdsManager : MonoBehaviour
{
    private BannerView bannerView;
    private List<BannerView> BVLIST;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        //StartCoroutine("RequestBanner");
        this.RequestBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RequestBanner()
    {
        //yield return new WaitForSeconds(3);
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-1778345177688333/2649166335";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        if(PlayerPrefs.GetInt("NoAds") == 0 )
        {
            // Create a 320x50 banner at the top of the screen.
            this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
            BVLIST.Add(bannerView);

            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            this.bannerView.LoadAd(request);
        }
    }
    public void DestroyBanner()
    {
        bannerView.Hide();
    }
}
