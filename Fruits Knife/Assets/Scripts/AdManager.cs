
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class AdManager : MonoBehaviour
{
    //APP ID
    public string appID;

    // Banner
    public string bannerID;
    private BannerView banner;

    // Interstital
    public string interstitialID;
    private InterstitialAd interstitial;

    //Reward Video
    public string videoID;
    private RewardBasedVideoAd video;


    public bool isPublish;


    public delegate void OnRewardVideoListener();
    public static event OnRewardVideoListener OnRewardVideo;

    private void Awake()
    {
        if (PlayerPrefs.GetString("RemoveADS", "false").Equals("false"))
        {
            if (isPublish)
                MobileAds.Initialize(appID);
            else
            {
                bannerID = "ca-app-pub-3940256099942544/6300978111";
                interstitialID = "ca-app-pub-3940256099942544/1033173712";
                videoID = "ca-app-pub-3940256099942544/5224354917";
            }

            banner = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Bottom);
            interstitial = new InterstitialAd(interstitialID);
            video = RewardBasedVideoAd.Instance;
            RequestBanner();
            RequestInterstitial();
            RequestVideo();
        }
        else
            Destroy(this.gameObject);
           
    }



    private void OnEnable()
    {
        if (PlayerPrefs.GetString("RemoveADS", "false").Equals("false"))
            Subscribe(true);
    }

    private void OnDisable()
    {
        if (PlayerPrefs.GetString("RemoveADS", "false").Equals("false"))
            Subscribe(false);
    }




    private void Subscribe(bool sub)
    {
        if(sub)
        {
        banner.OnAdLoaded += OnBannerLoaded;
        banner.OnAdFailedToLoad += OnBannerFailedToLoad;
        interstitial.OnAdFailedToLoad += OnInterstitialFailedToLoad;
        video.OnAdFailedToLoad += OnVideoFailedToLoad;
        video.OnAdClosed += OnVideoClosed;
        video.OnAdRewarded += OnVideoRewarded;
        }
        else
        {
            banner.OnAdLoaded -= OnBannerLoaded;
            banner.OnAdFailedToLoad -= OnBannerFailedToLoad;
            interstitial.OnAdFailedToLoad -= OnInterstitialFailedToLoad;
            video.OnAdFailedToLoad -= OnVideoFailedToLoad;
            video.OnAdClosed -= OnVideoClosed;
            video.OnAdRewarded -= OnVideoRewarded;

        }
     
    }

    

    

   



    //banner
    private void RequestBanner()
    {
        AdRequest adRequest = new AdRequest.Builder().Build();
        banner.LoadAd(adRequest);
    }
    private void ShowBanner()
    {
        banner.Show();
    }
    private void OnBannerFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestVideo();
    }
    private void OnBannerLoaded(object sender, EventArgs e)
    {
        banner.Show();
    }




    //interstitial
    private void RequestInterstitial()
    {
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitial.LoadAd(adRequest);
    }
    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
        else
            RequestInterstitial();
    }
   
    private void OnInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestInterstitial();
    }




    //video
    private void RequestVideo()
    {
        AdRequest adRequest = new AdRequest.Builder().Build();
        video.LoadAd(adRequest, videoID);
    }
    public void ShowVideo()
    {
        if (video.IsLoaded())
            video.Show();
        else
            RequestVideo();
    }
    private void OnVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestVideo();
    }
    private void OnVideoClosed(object sender, EventArgs e)
    {
        RequestVideo();
    }
    private void OnVideoRewarded(object sender, Reward e)
    {
        if (OnRewardVideo != null)
            OnRewardVideo();

    }
    
}
