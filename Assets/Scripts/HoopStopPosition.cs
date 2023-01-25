using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopStopPosition : MonoBehaviour
{
    public bool Visible;
    // Start is called before the first frame update
    void Start()
    {
        LevelHandler.instance.visibleMovement = false;
        Visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameVisible()
    {
        LevelHandler.instance.visibleMovement = true;
        Visible = true;
    }
}
