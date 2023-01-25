using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public float HoopPosX;
    public GameObject player;
    public Vector2 startPos;
    public GameObject[] Hooks;
    // Start is called before the first frame update
    void Start()
    {
        startPos = player.transform.position;
        DisablePlayeratStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartLevel()
    {
        DisablePlayeratStart();
        player.transform.position = startPos;
        player.SetActive(true);

    }
    public void DisablePlayeratStart()
    {
        player.GetComponent<PlayerController>().DisablePlayer();
        player.GetComponent<PlayerController>().enabled = false;
    }
    public void EnablePlayeratStart()
    {
       
        player.GetComponent<PlayerController>().enabled = true;
    }
}
