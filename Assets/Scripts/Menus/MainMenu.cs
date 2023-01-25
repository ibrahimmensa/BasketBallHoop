using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loading;
    public Slider slider;
    [Header("LevelNumText")]
    public Text levelNum;
    public GameObject zeroNum;
    public int num;
    public GameObject levelcount;
    [Header("SHOP_MENUS")]
    public GameObject settingPanel;
    public GameObject shopPanel;
    public GameObject challengePanel;
    public GameObject ballsPanel;
    //public GameObject tutorial;
    public GameObject fadescreen;
    public static MainMenu instance;

    [Header("CoinsText")]
    public Text coins;
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.SetInt("Coins",0);
        instance = this;
        StartCoroutine(Load());
        SoundsManager.instance.MainMenuSounds();
        levelNum.text = num.ToString();
        coins.text = amount.ToString();
        amount = PlayerPrefs.GetInt("Coins");
        //if (PlayerPrefs.GetInt("Tutorial") == 1)
        //{
        //    tutorial.SetActive(false);
        //}
        //else
        //{
        //    Invoke("activetutorial", 0.3f);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        levelNum.text = num.ToString();  
    }
    public void AssignCoins()
    {
        coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }
    public void TapToPlay()
    {
       
        MenuHandler.Instance.StartLevelPlay();
        SoundsManager.instance.GamePlaySounds();
    }
    /*-------------------------------------------------------
     * -----------------COINS-----------------------
     * -----------------------------------------------------*/
    public void giveCoins()
    {
       // amount = PlayerPrefs.GetInt("Coins"); 
        amount = PlayerPrefs.GetInt("Coins", 0) + 100;
        PlayerPrefs.SetInt("Coins",amount);
        AssignCoins();
    }

    /*-------------------------------------------------------
     * -----------------LOADING SCREEN-----------------------
     * -----------------------------------------------------*/
    IEnumerator Load()
    {

        yield return new WaitForSeconds(0.2f);
        slider.value = 0.2f;
        yield return new WaitForSeconds(0.2f);
        slider.value = 0.4f;
        yield return new WaitForSeconds(0.2f);
        slider.value = 0.6f;
        yield return new WaitForSeconds(0.2f);
        slider.value = 0.8f;
        yield return new WaitForSeconds(0.2f);
        slider.value = 1f;
        yield return new WaitForSeconds(0.2f);
        loading.SetActive(false);
    }

    /*-------------------------------------------------------
     * -----------------SHOP MENU-----------------------
     * -----------------------------------------------------*/
    public void SETTING()
    {
        levelcount.SetActive(false);
        settingPanel.SetActive(true);
    }
    public void SHOP()
    {
        levelcount.SetActive(false);
        shopPanel.SetActive(true);
    }
    public void CHALLENGE()
    {
        levelcount.SetActive(false);
        challengePanel.SetActive(true);
    }
    public void BALLS()
    {
        levelcount.SetActive(false);
        ballsPanel.SetActive(true);
    }

    //public void TutorialPanelFalse(bool toggle)
    //{
    //    if (toggle)
    //    {
    //        PlayerPrefs.SetInt("Tutorial", 0);
    //        tutorial.SetActive(true);
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetInt("Tutorial", 1);
    //        tutorial.SetActive(true);
    //    }

    //    tutorial.SetActive(false);

    //}

    //public void activetutorial()
    //{
       /// tutorial.SetActive(true);
   // }

    public void URLs(string url)
    {
        Application.OpenURL(url);
    }
    
    
}
