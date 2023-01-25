using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    
    
    public AudioSource MainMenu;
    public AudioSource GamePlay;
    public AudioSource LevelComp;
    public AudioSource LevelFailed;
    public AudioSource vibration;
    public AudioSource bounce;
    public static SoundsManager instance;
    [Header("Buttons")]
    public GameObject soundOff;
    public GameObject soundOn;
    public GameObject VibOff;
    public GameObject VibOn;
    // Start is called before the first frame update
    void Awake()
    {
        //if (instance == null)
        //{
            instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundoff();
        }
       else if (PlayerPrefs.GetInt("Sound") == 0)
        {
            soundON();
        }
        StartVibration();
    }
    
    public void MainMenuSounds()
    {
        MainMenu.Play();
        GamePlay.Pause();
    }
    public void GamePlaySounds()
    {
        GamePlay.Play();
        MainMenu.Pause();
       
    }
    public void GameOver()
    {
       LevelFailed.Play();
        GamePlay.Pause();
        MainMenu.Pause();
        LevelComp.Pause();
       

    }
    public void BounceSound()
    {
        bounce.Play();
    }
  
    public void LevenWin()
    {
        
        LevelComp.Play();
        LevelFailed.Stop();
        GamePlay.Stop();
        MainMenu.Stop();

    }
  
    public void soundoff()
    {
        if (soundOff)
        {
            soundOff.SetActive(true);

        }
        if (soundOn)
        {
            soundOn.SetActive(false);

        }

        PlayerPrefs.SetInt("Sound", 1);
        LevelComp.volume = 0;
        LevelFailed.volume = 0;
        GamePlay.volume = 0;
        MainMenu.volume = 0;
        bounce.volume = 0;
    }
    public void soundON()
    {
        if (soundOff)
        {
            soundOff.SetActive(false);

        }
        if (soundOn)
        {
            soundOn.SetActive(true);

        }
        PlayerPrefs.SetInt("Sound", 0);
        LevelComp.volume = 1;
        LevelFailed.volume = 1;
        GamePlay.volume = 1;
        MainMenu.volume = 1;
        bounce.volume = 1;
    }
    public void StartVibration()
    {
        if(PlayerPrefs.GetInt("vibrate", 0) == 0)
        {
            Handheld.Vibrate();
            VibOff.SetActive(false);
            VibOn.SetActive(true);
        }
        else
        {
            VibOff.SetActive(true);
            VibOn.SetActive(false);
        }

    }
    public void VibrationButton()
    {
        if (PlayerPrefs.GetInt("vibrate", 0) == 1)
        {
            PlayerPrefs.SetInt("vibrate", 0);
            VibOff.SetActive(false);
            VibOn.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("vibrate", 1);
            VibOff.SetActive(true);
            VibOn.SetActive(false);
        }
    }
}
