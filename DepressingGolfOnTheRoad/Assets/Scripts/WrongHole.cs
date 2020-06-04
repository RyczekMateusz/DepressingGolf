using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongHole : MonoBehaviour
{

    public GameObject ball;
    public Vector3 lastBallPos = new Vector3(0f, 0.019f, 0f);
    public Rigidbody playerBallRB;

    IEnumerator RespawnBall()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        playerBallRB.velocity = new Vector3(0, 0, 0);
        ball.transform.position = lastBallPos;
        //ball.layer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        playerBallRB = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBallRB.IsSleeping() && ball.transform.position.y > -0.03f)
        {
            lastBallPos = ball.transform.position;

            //GameObject.Find("Player").transform.position;
        }

        if (ball.layer == 8 && ball.transform.position.y<-0.06f)
        {
            StartCoroutine(RespawnBall());
            
        }
    }
}
