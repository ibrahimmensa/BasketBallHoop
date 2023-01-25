using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsScript : MonoBehaviour
{
    public List<GameObject> Balls;
   
    public void ChangeBall( int i )
    {
        
        PlayerPrefs.SetInt("Ball", i);
        LevelHandler.instance.BallChanged();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
