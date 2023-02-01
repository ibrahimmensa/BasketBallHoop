using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBall : MonoBehaviour
{
    public int BallId, Coins, AdsToWatch;
    public bool Bought;
    public GameObject CoinsText, BuyButton, SelectButton, BuyButtonWithAds;
    // Start is called before the first frame update
    void Start()
    {
        
        
        if(BallId == 0)
        {
            Bought = true;
        }
        else
        {
            if (PlayerPrefs.GetString("BallNo" + BallId.ToString(), "false") == "true")
            {
                Bought = true;
                DisableButtons();
            }
            else
            {
                if (!PlayerPrefs.HasKey("BallNo" + BallId.ToString() + "remaining ads"))
                {
                    PlayerPrefs.SetInt("BallNo" + BallId.ToString() + "remaining ads", AdsToWatch);
                }
                Bought = false;
                EnableButtons();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyNow()
    {
        if(PlayerPrefs.GetInt("Coins", 0) >= Coins)
        {
            GameAnalytics.NewDesignEvent("ButtonClickedEvents:BallSelectionScreen:BuyWithCoinsButton");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - Coins);
            PlayerPrefs.SetString("BallNo" + BallId.ToString(), "true");
            MainMenu.instance.AssignCoins();
            DisableButtons();
        }
        
    }
    public void DisableButtons()
    {
        CoinsText.SetActive(false);
        BuyButton.SetActive(false);
        BuyButtonWithAds.SetActive(false);
        SelectButton.GetComponent<Button>().interactable = true;
    }
    public void EnableButtons()
    {
        CoinsText.SetActive(true);
        BuyButton.SetActive(true);
        BuyButtonWithAds.SetActive(true);
        BuyButtonWithAds.transform.GetChild(0).gameObject.GetComponent<Text>().text = (AdsToWatch - PlayerPrefs.GetInt("BallNo" + BallId.ToString() + "remaining ads", AdsToWatch)).ToString() + "/" + AdsToWatch.ToString();
        SelectButton.GetComponent<Button>().interactable = false;
    }

    public void BuyWithAds()
    {
        GameAnalytics.NewDesignEvent("ButtonClickedEvents:BallSelectionScreen:BuyWithAdsButton");
        UnityAdsManager.Instance.GetFreeBall(this.gameObject);
    }
    public void AdCompleted()
    {
        PlayerPrefs.SetInt("BallNo" + BallId.ToString() + "remaining ads", 
            PlayerPrefs.GetInt("BallNo" + BallId.ToString() + "remaining ads", AdsToWatch) - 1);
        if (PlayerPrefs.GetInt("BallNo" + BallId.ToString() + "remaining ads", AdsToWatch) == 0)
        {
            PlayerPrefs.SetString("BallNo" + BallId.ToString(), "true");
            DisableButtons();
        }
        else
        {
            BuyButtonWithAds.transform.GetChild(0).gameObject.GetComponent<Text>().text = (AdsToWatch - PlayerPrefs.GetInt("BallNo" + BallId.ToString() + "remaining ads", AdsToWatch)).ToString() + "/" + AdsToWatch.ToString();
        }
    }
}
