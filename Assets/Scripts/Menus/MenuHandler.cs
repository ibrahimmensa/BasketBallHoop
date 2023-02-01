using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : Singleton<MenuHandler>
{
    public GameObject mainMenu, GamePlay;
    public bool GamePasued;
    // Start is called before the first frame update
    void Start()
    {
        GamePasued = false;
    }

    // Update is called once per frame
    void Update()
    {
        MainMenu.instance.coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }
    public void StartLevelPlay()
    {
        LevelHandler.Instance.Levels[LevelHandler.Instance.CurrentLevel].GetComponent<LevelData>().EnablePlayeratStart();
        mainMenu.SetActive(false);
        GamePlay.SetActive(true);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_" + LevelHandler.Instance.CurrentLevel, "Level_Progress");
    }
    public void ReturnToHome()
    {
        GameAnalytics.NewDesignEvent("ButtonClickedEvents:PauseScreen:BackToMainMenuButton");
        LevelHandler.Instance.ResetLevelData();
        mainMenu.SetActive(true);
        GamePlay.SetActive(false);
        ResumeGame();
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        GameAnalytics.NewDesignEvent("ButtonClickedEvents:PauseScreen:RestartButton");
        ResumeGame();
        //PlayerController.instance.StartCoroutine("levelFailedDelay");
        PlayerController.instance.ball = 0;
        LevelHandler.Instance.ResetLevelData();
        LevelHandler.Instance.Levels[LevelHandler.Instance.CurrentLevel].GetComponent<LevelData>().EnablePlayeratStart();
        mainMenu.SetActive(false);
        GamePlay.SetActive(true);
    }
    public void ResumeGame()
    {
        GameAnalytics.NewDesignEvent("ButtonClickedEvents:PauseScreen:ResumeButton");
        GamePasued = false;
        LevelHandler.instance.ResumeLevel();
    }
    public void PauseGame()
    {
        GameAnalytics.NewDesignEvent("ButtonClickedEvents:GameplayScreen:PauseButton");
        GamePasued = true;
        LevelHandler.instance.PauseLevel();
    }

    
}
