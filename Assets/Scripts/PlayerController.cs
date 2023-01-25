using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrabbing;
    public bool GameStarted;
    public Vector3 lastVelocity;
    public Material lineRendererMaterial;
    public List<Sprite> BasketBalls;
    public int ball = 0;
    Vector3 StartingPosition;
    LineRenderer lr;
    public bool stayingInJump = false, inJumpRoutine = false;

    public static PlayerController instance;
    private void OnEnable()
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = BasketBalls[PlayerPrefs.GetInt("Ball", 0)];
    }
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        gameObject.GetComponent<SpriteRenderer>().sprite = BasketBalls[PlayerPrefs.GetInt("Ball", 0)];
        DisablePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        lr = gameObject.GetComponent<LineRenderer>();
        //lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.material = lineRendererMaterial;
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.SetWidth(0.05f, 0.05f);
        if (Input.GetMouseButtonDown(0) && !MenuHandler.Instance.GamePasued)
        {
            isGrabbing = true;
            EnablePlayer();
            StartCoroutine("delayRoutine");
        }
        if(Input.GetMouseButton(0) && GameStarted && !MenuHandler.Instance.GamePasued)
        {
            
            lr.positionCount = 2;
            GameObject closest = findClosest();
            if (isGrabbing)
            {
                lr.SetPosition(1, closest.transform.position);
                closest.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
                isGrabbing = false;
                gameObject.GetComponent<Rigidbody2D>().AddForce ( new Vector2(1000,0));
            }
            lr.SetPosition(0, transform.position);
        }
        if (Input.GetMouseButtonUp(0) && GameStarted && !MenuHandler.Instance.GamePasued)
        {
            //GameObject[] hinges;
            //hinges = GameObject.FindGameObjectsWithTag("Hinge");
            lr.positionCount = 0;
            foreach (GameObject OBJ in LevelHandler.Instance.Levels[LevelHandler.Instance.CurrentLevel].GetComponent<LevelData>().Hooks)
            {
                OBJ.GetComponent<HingeJoint2D>().connectedBody = null;
            }
        }
        





        //Debug.Log("LG >> In Update");

        if (Input.touchCount > 0 && !MenuHandler.Instance.GamePasued)
        {
            Debug.Log("LG >> Touch Detected");
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began && !MenuHandler.Instance.GamePasued)
            {
                isGrabbing = true;
                //EnablePlayer();
                StartCoroutine("delayRoutine");
            }
            if (touch.phase == TouchPhase.Stationary && GameStarted && !MenuHandler.Instance.GamePasued)
            {

                lr.positionCount = 2;
                GameObject closest = findClosest();
                if (isGrabbing)
                {
                    lr.SetPosition(1, closest.transform.position);
                    closest.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
                    isGrabbing = false;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(150, 0));
                }
                lr.SetPosition(0, transform.position);
            }
            if (touch.phase == TouchPhase.Ended && GameStarted && !MenuHandler.Instance.GamePasued)
            {
                OnclickExit();
            }
        }
        

        if(MenuHandler.Instance.GamePasued)
        {
            lr.positionCount = 0;
        }

    }
 public void OnclickExit()
    {
        //GameObject[] hinges;
        //hinges = GameObject.FindGameObjectsWithTag("Hinge");
        gameObject.GetComponent<LineRenderer>().positionCount = 0;
        foreach (GameObject OBJ in LevelHandler.Instance.Levels[LevelHandler.Instance.CurrentLevel].GetComponent<LevelData>().Hooks)
        {
            OBJ.GetComponent<HingeJoint2D>().connectedBody = null;
        }
        
    }
    GameObject findClosest()
    {
        GameObject[] hinges;
        hinges = GameObject.FindGameObjectsWithTag("Hinge");
        GameObject CLosest = null;
        float distance = Mathf.Infinity;
        Vector3 ObjPosition = transform.position;

        foreach (GameObject OBJ in hinges)
        {
            Vector3 diff = OBJ.transform.position - ObjPosition;
            float currentDis = diff.sqrMagnitude;
            if(currentDis < distance)
            {
                CLosest = OBJ;
                distance = currentDis;
            }
        }
        return CLosest;
    }
    public void FinishFunc()
    {
       
        //gameObject.SetActive(false);
        //gameObject.GetComponent<HingeJoint2D>().breakForce = 2;
        StartCoroutine("FinishdelayRoutine");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "Finish")
        {
            DisappearBalls();
            FinishFunc();
        }
        if (collision.transform.tag == "Respawn")
        {
            SoundsManager.instance.StartVibration();
            //  SoundsManager.instance.GameOver();  
            LevelFailed();
            //gameObject.SetActive(false);
        }
        if (collision.transform.tag == "Ball")
        {
            Debug.Log("Collided with ball");
            BallJoint(collision);
        }
        if (collision.transform.tag == "Tunnel")
        {
            Debug.Log("Collided with tunnel");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Mathf.Max(collision.transform.gameObject.GetComponent<TunnelSpeedManager>().Tunnelspeed, 2);
        }
    }
    public void BallJoint(Collider2D collision)
    {
        Destroy(collision.gameObject);
        gameObject.transform.GetChild(ball).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(ball).gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.transform.GetChild(ball).gameObject.GetComponent<Rigidbody2D>().simulated = true;
        //gameObject.transform.GetChild(ball).gameObject.SetActive(true);
        ball++;
    }
    public void DisappearBalls()
    {
        for(int i = 0; i < 3; i++)
        {
            gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(i).gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Jump")
    //    {
    //        findClosest().GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
    //    }
    //}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Jump")
        {
            SoundsManager.instance.BounceSound();
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            gameObject.GetComponent<Rigidbody2D>().velocity = direction * Mathf.Max(speed, 10f);
        }
        if (collision.transform.tag == "Bounce")
        {
            SoundsManager.instance.BounceSound();
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            gameObject.GetComponent<Rigidbody2D>().velocity = direction * Mathf.Max(speed, 2.5f);
            
        }
        if (collision.transform.tag == "Ground")
        {
            SoundsManager.instance.StartVibration();
            
            StartCoroutine("levelFailedDelay");

        }
       
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Jump")
        {
            if(!inJumpRoutine)
            {
                stayingInJump = true;
                StartCoroutine("straightJump");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Jump")
        {
            stayingInJump = false;
        }
    }
    IEnumerator straightJump()
    {
        inJumpRoutine = true;
        yield return new WaitForSeconds(0.15f);
        if(stayingInJump)
        {
            SoundsManager.instance.BounceSound();
            var speed = lastVelocity.magnitude;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.up * Mathf.Max(20, 20);
        }
        inJumpRoutine = false;
    }

    public void DisablePlayer()
    {
        gameObject.GetComponent<Rigidbody2D>().mass = 0;
        gameObject.GetComponent<LineRenderer>().positionCount = 0;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        GameStarted = false;
    }
    public void EnablePlayer()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = BasketBalls[PlayerPrefs.GetInt("Ball", 0)];
        for (int i = 0; i < 3; i++)
        {
            gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = BasketBalls[PlayerPrefs.GetInt("Ball", 0)];
        }
        gameObject.GetComponent<Rigidbody2D>().mass = 5;
    }
    IEnumerator delayRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        GameStarted = true;
        //EnablePlayer();
    }
    IEnumerator FinishdelayRoutine()
    {
        SoundsManager.instance.LevenWin();

        Debug.Log("Enters couroutine");
        if (LevelHandler.instance.CurrentLevel >=LevelHandler.instance.Levels.Count)
        {
            LevelHandler.instance.CurrentLevel = 0;
            PlayerPrefs.SetInt("Current Level", LevelHandler.instance.CurrentLevel);
        }
        yield return new WaitForSeconds(1.4f);
        LevelHandler.Instance.MoveToNextLevel();
        MainMenu.instance.giveCoins();
        UnityAdsManager.Instance.ShowAdInterstitial();
        //LevelHandler.Instance.MoveToNextLevel();
        MainMenu.instance.giveCoins();
        gameObject.SetActive(false);
    }

    public void LevelFailed()
    {
        StartCoroutine("levelFailedDelay");
    }

    IEnumerator levelFailedDelay()
    {
        
        SoundsManager.instance.GameOver();
        yield return new WaitForSeconds(1.4f);
        UnityAdsManager.Instance.ShowAdInterstitial();
        gameObject.SetActive(false);
        MenuHandler.Instance.ReturnToHome();
        DisablePlayer();
      
    }
   
}
