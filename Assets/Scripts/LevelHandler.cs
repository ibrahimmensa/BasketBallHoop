using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class LevelHandler : Singleton<LevelHandler>
{
    public List<GameObject> Levels;
    public int CurrentLevel;
    public GameObject CurrentPlayer;
    public float pos;
    public GameObject cam;
    public bool visibleMovement;

    public static LevelHandler instance;
    // Start is called before the first frame update
    void Start()
    {
        visibleMovement = false;
        //Screen.SetResolution(1080, 1920,true);
        instance = this;
        CurrentLevel = PlayerPrefs.GetInt("Current Level", 0);
        //CurrentLevel = 1;
        ActiveCurrentLevel();
        CurrentPlayer = Levels[CurrentLevel].GetComponent<LevelData>().player;
        pos = Levels[CurrentLevel].GetComponent<LevelData>().HoopPosX;
        if (CurrentLevel >=1)
        {
            MainMenu.instance.loading.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public float ReturnEndPos()
    {
        return Levels[CurrentLevel].GetComponent<LevelData>().HoopPosX;
    }
    public void ActiveCurrentLevel()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
          
            if (i == CurrentLevel)
            {
                //Levels[i].SetActive(false);
                Levels[i].SetActive(true);
                MainMenu.instance.num = i+1;
                if (i>=9)
                {
                    MainMenu.instance.zeroNum.SetActive(false);
                }
            }
            else
            {
                Levels[i].SetActive(false);
            }
           
        }
    }


    public void MoveToNextLevel()
    {
      
        CurrentLevel = CurrentLevel + 1 % Levels.Count;
       
        if (CurrentLevel==Levels.Count)
        {
            CurrentLevel = 0;
        }
        PlayerPrefs.SetInt("Current Level", CurrentLevel);
        ActiveCurrentLevel();
        ResetLevelData();
        MenuHandler.Instance.ReturnToHome();
        
      
    }  
   
    public void ResetLevelData()
    {
        cam.GetComponent<CameraFollow>().resetPosition();
        cam.GetComponent<CameraFollow>().GetStopPosition();
        CurrentLevel = PlayerPrefs.GetInt("Current Level", 0);
        //CurrentLevel = 2;
        ActiveCurrentLevel();
        CurrentPlayer = Levels[CurrentLevel].GetComponent<LevelData>().player;
        pos = Levels[CurrentLevel].GetComponent<LevelData>().HoopPosX;
        Levels[CurrentLevel].GetComponent<LevelData>().RestartLevel();
       
       
    }
    public void PauseLevel()
    {
        Levels[CurrentLevel].GetComponent<LevelData>().DisablePlayeratStart();
    }
    public void ResumeLevel()
    {
        Levels[CurrentLevel].GetComponent<LevelData>().EnablePlayeratStart();
    }
    public void BallChanged()
    {
        Levels[CurrentLevel].GetComponent<LevelData>().player.GetComponent<SpriteRenderer>().sprite =
                Levels[CurrentLevel].GetComponent<LevelData>().player.GetComponent<PlayerController>().BasketBalls[PlayerPrefs.GetInt("Ball", 0)]; 
        for (int i = 0; i < 3; i++)
        {
            Levels[CurrentLevel].GetComponent<LevelData>().player.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite =
                Levels[CurrentLevel].GetComponent<LevelData>().player.GetComponent<PlayerController>().BasketBalls[PlayerPrefs.GetInt("Ball", 0)];
        }
    }

    
}
