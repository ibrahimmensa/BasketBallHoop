using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float StopPosition;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GetStopPosition", 0.2f);
        //Debug.Log(Camera.main.aspect);
    }

    // Update is called once per frame
    void Update()
    {
        float position = LevelHandler.Instance.CurrentPlayer.transform.position.x + 1.7f;
        if (position < StopPosition)//(!LevelHandler.instance.visibleMovement)
        {
            gameObject.transform.position = new Vector3(LevelHandler.Instance.CurrentPlayer.transform.position.x + 1.7f, 0, -10);
        }
    }
    public void GetStopPosition()
    {
        StopPosition = LevelHandler.Instance.ReturnEndPos();
        if(Camera.main.aspect < 0.5f)
        {
            StopPosition = StopPosition + 0.9f;
        }
    }
    public void resetPosition()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
