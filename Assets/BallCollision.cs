using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = PlayerController.instance.BasketBalls[PlayerPrefs.GetInt("Ball", 0)];
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.transform.tag == "Ball")
        {
            Debug.Log("Collided with ball");
            PlayerController.instance.BallJoint(collision);
        }

        if (collision.transform.tag == "Finish")
        {
            //PlayerController.instance.FinishFunc();
            //gameObject.GetComponent<HingeJoint2D>().breakForce = 2;
        }

        if (collision.transform.tag == "Respawn")
        {
            PlayerController.instance.LevelFailed();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            SoundsManager.instance.StartVibration();
            PlayerController.instance.LevelFailed();
        }
        if (collision.transform.tag == "Respawn")
        {
            PlayerController.instance.LevelFailed();
        }
    }

}
