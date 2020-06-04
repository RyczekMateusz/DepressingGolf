using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject arrow = GameObject.FindGameObjectWithTag("StrokeInd");
        FindPlayerBall();
        StrokeCount = 0;
        
    }

    public float StrokeAngle { get; protected set; }
    public float StrokeForce { get; protected set; }
    public float StrokeForcePerc { get { return StrokeForce / MaxStrokeForce; } }


    public int StrokeCount { get; protected set; }
    public GameObject arrow { get; protected set; }

    float strokeForceFillSpeed = 5f;

    int fillDir = 1;

    float MaxStrokeForce=5f;

    public enum StrokeModeEnum { AIMING, FILLING, READY_TO_WHACK, BALL_IS_ROLLING }
    public StrokeModeEnum StrokeMode { get; protected set; }

    Rigidbody playerBallRB;

    private void FindPlayerBall()
    {
       
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go == null)
        {
            Debug.LogError("Couldn't find the ball.");
        }
        playerBallRB = go.GetComponent<Rigidbody>();

        if (playerBallRB == null)
        {
            Debug.LogError("Ball doesn't have rigidbody");
        }
    }

    // Update is called once per frame
    private void Update()
    {


        if (StrokeMode == StrokeModeEnum.AIMING)
        {
            
            GameObject.Find("3DArrow").transform.localScale = new Vector3(0.03f, 0.03f, 0.01f);
            StrokeAngle += Input.GetAxis("Horizontal") * 100f * Time.deltaTime;

            if (Input.GetButtonUp("Fire"))
            {
                StrokeMode = StrokeModeEnum.FILLING;
                return;
            }
        }

        if(StrokeMode == StrokeModeEnum.FILLING)
        {
            StrokeForce += (strokeForceFillSpeed * fillDir)* Time.deltaTime;
            if(StrokeForce > MaxStrokeForce)
            {
                StrokeForce = MaxStrokeForce;
                fillDir = -1;
            }
            else if (StrokeForce < 0)
            {
                StrokeForce = 0;
                fillDir = 1;
            }

            if (Input.GetButtonUp("Fire"))
            {
                GameObject.Find("3DArrow").transform.localScale = new Vector3(0f, 0f, 0f);
                StrokeMode = StrokeModeEnum.READY_TO_WHACK;
            }
        }
    }

    void CheckRollingStatus()
    {
        
        if (playerBallRB.IsSleeping())
        {
            StrokeMode = StrokeModeEnum.AIMING;
        }
    }


    void FixedUpdate()
    {
        //GameObject arrow = GameObject.FindGameObjectWithTag("StrokeInd");
        if (playerBallRB == null)
        {
            return;
        }

        if (StrokeMode == StrokeModeEnum.BALL_IS_ROLLING)
        {
            CheckRollingStatus();
            return;
        }

        if(StrokeMode != StrokeModeEnum.READY_TO_WHACK)
        {
            return;
        }


        Vector3 forceVec = new Vector3(0, 0, StrokeForce);
            
        playerBallRB.AddForce(Quaternion.Euler(0, StrokeAngle, 0) * forceVec, ForceMode.Impulse);
        StrokeForce = 0;
        StrokeCount++;
        
        fillDir = 1;
        StrokeMode = StrokeModeEnum.BALL_IS_ROLLING;

    }
}
