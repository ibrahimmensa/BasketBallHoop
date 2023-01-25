using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Movement : MonoBehaviour
{
    public GameObject ObjectToMove;
    public Transform Start_point;
    public Transform End_point;
    public float speed;

    bool started;
    private void Start()
    {
        Start_point.position = ObjectToMove.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(ObjectToMove.transform.position, End_point.position) < 0.01f)
        {
            if (!started)
            {
                //ObjectToMove.transform.rotation = End_point.rotation;
                started = true;
            }
            else
            {
                // ObjectToMove.transform.rotation = Start_point.rotation;
                started = false;
            }
            End_point.position = Start_point.position;
            Start_point.position = ObjectToMove.transform.position;
        }
        ObjectToMove.transform.position =
            Vector2.MoveTowards(ObjectToMove.transform.position, End_point.position, speed);
    }
}
