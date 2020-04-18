using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersScript : MonoBehaviour
{  
    public float speed;
    public KeyCode tapKey;
    public Rigidbody player;
    public Vector3 playerOffset;
    public bool isFinish;
    public int playerNumber;
    private int sceneIndex;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        GameManager.Instance.playerRank[playerNumber - 1].tapKey = tapKey;
    }

    private void Update()
    {
        if (GameManager.Instance.play == true)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == 1)
            {
                RunningStage();
            }
            else if (sceneIndex == 2)
            {

            }
            else if (sceneIndex == 3)
            {

            }
        }
    }

    void RunningStage() {
        if (!isFinish)
        {
            if (Input.GetKeyDown(tapKey))
            {
                speed += 1f;
            }
            else
            {
                if (speed > 0)
                    speed = Mathf.Abs(speed) - .1f;
                else
                    speed = 0;
            }
            Vector3 tempVect = new Vector3(1, 0, 0);
            tempVect = tempVect.normalized * speed * Time.deltaTime;
            player.MovePosition(transform.position + tempVect);
        }
        else
        { 
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        isFinish = true;
        int i = 0;
        GameManager.Instance.playerFinish[GameManager.Instance.countFinish] = playerNumber;
        GameManager.Instance.countFinish++;
        while (playerNumber != GameManager.Instance.playerFinish[i])
        {
            i++;
        }
        if (i == 0)
        {
            GameManager.Instance.playerRank[playerNumber - 1].score = 200000f;
        }
        else if (i == 1)
        {
            GameManager.Instance.playerRank[playerNumber - 1].score = 150000f;
        }
        else if (i == 2)
        {
            GameManager.Instance.playerRank[playerNumber - 1].score = 100000f;
        }
        else if (i == 3)
        {
            GameManager.Instance.playerRank[playerNumber - 1].score = 50000f;
        }
    }



}
