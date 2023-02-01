using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.IO;
using GameAnalyticsSDK;

public class InAppManager : MonoBehaviour
{
    public string Remove_Ads = "";
    public string Coins500 = "";
    public string Coins1500 = "";
    public string Coins5000 = "";
    public string Coins7000 = "";
    public GameObject NoadsButton;
    bool startDisabling = true;
    //public string Coins10000 = "";
    int Coins;
    public GameObject AdsManagerRef;
    private void Awake()
    {
        StartCoroutine("stopDiabling");
        if (CodelessIAPStoreListener.Instance.StoreController != null)
        {
            Debug.Log("Entered receipt");
            Product product = CodelessIAPStoreListener.Instance.StoreController.products.WithID(Remove_Ads);
            //Product product1 = CodelessIAPStoreListener.Instance.StoreController.products.WithID(Coins7000);
            if (product==null)
            {
                Debug.Log("shit");
            }
            if (product.hasReceipt)
            {
                NoadsButton.SetActive(false);
                PlayerPrefs.SetInt("NoAds", 1);
                AdsManagerRef.GetComponent<AdmobAdsManager>().DestroyBanner();
            }
        }
        if (PlayerPrefs.GetInt("NoAds", 0) == 1)
        {
            NoadsButton.SetActive(false);
            AdsManagerRef.GetComponent<AdmobAdsManager>().DestroyBanner();
        }
        //string json = File.ReadAllText(Application.persistentDataPath + "/AdDataFile.txt");
        //AdData data = JsonUtility.FromJson<AdData>(json);
        //if (data.Bought)
        //{
        //    NoadsButton.SetActive(false);
        //    PlayerPrefs.SetInt("NoAds", 1);
        //}
    }
    private void Start()
    {
        if (CodelessIAPStoreListener.Instance.StoreController != null)
        {
            Debug.Log("Entered receipt");
            Product product = CodelessIAPStoreListener.Instance.StoreController.products.WithID(Remove_Ads);
            if (product.hasReceipt)
            {
                NoadsButton.SetActive(false);
                PlayerPrefs.SetInt("NoAds", 1);
                AdsManagerRef.GetComponent<AdmobAdsManager>().DestroyBanner();
            }
        }
        if (PlayerPrefs.GetInt("NoAds", 0) == 1)
        {
            NoadsButton.SetActive(false);
            AdsManagerRef.GetComponent<AdmobAdsManager>().DestroyBanner();
        }
    }
    private void Update()
    {
        if(startDisabling)
        {
            if (CodelessIAPStoreListener.Instance.StoreController != null)
            {
                Debug.Log("Entered receipt");
                Product product = CodelessIAPStoreListener.Instance.StoreController.products.WithID(Remove_Ads);
                if (product.hasReceipt)
                {
                    NoadsButton.SetActive(false);
                    PlayerPrefs.SetInt("NoAds", 1);
                    AdsManagerRef.GetComponent<AdmobAdsManager>().DestroyBanner();
                }
            }
        }
        if (PlayerPrefs.GetInt("NoAds", 0) == 1)
        {
            NoadsButton.SetActive(false);
            AdsManagerRef.GetComponent<AdmobAdsManager>().DestroyBanner();
        }
    }
    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id.ToString()== Remove_Ads)
        {
            GameAnalytics.NewDesignEvent("ButtonClickedEvents:ShopScreen:RemoveAdsIAPButton");
            Ads();
        }

        if (product.definition.id.ToString() == Coins500)
        {
            GameAnalytics.NewDesignEvent("ButtonClickedEvents:ShopScreen:BuyCoinsIAPButton");
            CoinsBTN();
        }
        if (product.definition.id.ToString() == Coins1500)
        {
            GameAnalytics.NewDesignEvent("ButtonClickedEvents:ShopScreen:BuyCoinsIAPButton");
            CoinsBTNtwo();
        }
        if (product.definition.id.ToString() == Coins5000)
        {
            GameAnalytics.NewDesignEvent("ButtonClickedEvents:ShopScreen:BuyCoinsIAPButton");
            CoinsBTNthree();
        }
        if (product.definition.id.ToString() == Coins7000)
        {
            GameAnalytics.NewDesignEvent("ButtonClickedEvents:ShopScreen:BuyCoinsIAPButton");
            CoinsBTNFour();
        }
        //if (product.definition.id.ToString() == Coins10000)
        //{
        //    CoinsBTNFive();
        //}
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + "product name" + reason.ToString());
        if (product.definition.id.ToString() == Remove_Ads)
        {
            
        }
    }
    

    public void Ads()
    {
        //AdData data = new AdData();
        //data.Bought = true;
        //string json = JsonUtility.ToJson(data);
        //File.WriteAllText(Application.persistentDataPath + "/AdDataFile.txt", json);
        StartCoroutine("stopDiabling");
        PlayerPrefs.SetInt("NoAds", 1);
        PlayerPrefs.Save();
        NoadsButton.SetActive(false);
        AdsManagerRef.GetComponent<AdmobAdsManager>().DestroyBanner();
    }

    public void CoinsBTN()
    {
        Coins = 500;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + Coins);
        PlayerPrefs.GetInt("coins", MainMenu.instance.amount);
        MainMenu.instance.AssignCoins();
       
    }
    public void CoinsBTNtwo()
    {
        Coins = 1500;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + Coins);
        PlayerPrefs.GetInt("coins", MainMenu.instance.amount);
        MainMenu.instance.AssignCoins();
    }
    public void CoinsBTNthree()
    {
        Coins = 5000;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + Coins);
        PlayerPrefs.GetInt("coins", MainMenu.instance.amount);
        MainMenu.instance.AssignCoins();
    }
    public void CoinsBTNFour()
    {
        Coins = 7000;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + Coins);
        PlayerPrefs.GetInt("coins", MainMenu.instance.amount);
        //Ads();
        MainMenu.instance.AssignCoins();
    }
    public void CoinsBTNFive()
    {
        Coins = 10000;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + Coins);
        PlayerPrefs.GetInt("coins", MainMenu.instance.amount);

    }
    IEnumerator stopDiabling()
    {
        startDisabling = true;
        yield return new WaitForSeconds(10);
        startDisabling = false;
    }
    //public void Update()
    //{
    //  MainMenu.instance.coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    //}
}
public class AdData
{
    public bool Bought;
}
