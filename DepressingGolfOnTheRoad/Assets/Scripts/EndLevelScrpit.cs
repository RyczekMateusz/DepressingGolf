using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScrpit : MonoBehaviour
{

    public GameObject endLevel;
    public GameObject ball;
    public Rigidbody playerBallRB;

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.4f);
        endLevel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        playerBallRB = ball.GetComponent<Rigidbody>();
        endLevel.SetActive(false);

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (ball.layer == 9 && playerBallRB.IsSleeping())
        {
            StartCoroutine(EndLevel());
        }
    }



    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
