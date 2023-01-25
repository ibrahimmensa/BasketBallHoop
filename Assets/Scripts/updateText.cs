using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateText : MonoBehaviour
{
 
    [Header("CountText")]
     public Text countText;
     int number;
  
    private Vector2 startTouchposition;
    private Vector2 endTouchposition;

    // Start is called before the first frame update
    void Start()
    {
       // countText.text = number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchposition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchposition = Input.GetTouch(0).position;
            if (endTouchposition.x < startTouchposition.x)
            {
               
                Debug.Log("Increase");
               Incrementtext();
            }
            if (endTouchposition.x > startTouchposition.x)
            {
                 Debug.Log("Decrease");
                 Dcreasetext();
            }
        }
        countText.text = number.ToString();
    }
    private void Incrementtext()
    {
        number++;
        Debug.Log("IncreaseAAAAA");
    }
    private void Dcreasetext()
    {
        number--;
        Debug.Log("DecreaseDDDD");
    }
}
