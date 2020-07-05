using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersScript : MonoBehaviour
{  
    private float speed;
    public Color playerColor;  
    public KeyCode tapKey;
    public Rigidbody player;
    public Vector3 playerOffset;
    public Animator anim;
    public bool isFinish;
    public bool isStuned;
    public bool isTargeted;
    public int playerNumber;
    public string charName;
    private int sceneIndex;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        GameManager.Instance.playerRank[playerNumber - 1].tapKey = tapKey;
        GameManager.Instance.playerRank[playerNumber - 1].charName = charName;
        sceneIndex = SceneManager.GetActiveScene().buildIndex; ;
        playerColor = GetComponent<SpriteRenderer>().color;

        anim.SetBool("IsBossStage", false);
        anim.SetBool("IsRunningStage", false);

        if (sceneIndex == 2)
        {
            anim.SetBool("IsRunningStage", true);
        }
        else if (sceneIndex == 4)
        {
            anim.SetBool("IsBossStage", true);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.play == true)
        {
           
            if (sceneIndex == 2)
            {
                RunningStage();
            }

        }
    }

    void RunningStage() {
        if (!isFinish)
        {
            if (Input.GetKeyDown(tapKey))
            {
                speed += 1.5f;
            }
            else
            {
                if (speed > 0)
                {
                    speed = Mathf.Abs(speed) - .1f;
                    anim.SetBool("IsRunning", true);
                }
                else {
                    speed = 0;
                    anim.SetBool("IsRunning", false);
                }
            }
            Vector3 tempVect = new Vector3(1, 0, 0);
            if (GameManager.Instance.playerRank[playerNumber - 1].bufftype == "speed")
            {
                tempVect = tempVect.normalized * speed * GameManager.Instance.playerRank[playerNumber - 1].buffValue * Time.deltaTime;
            }
            else {
                tempVect = tempVect.normalized * speed * Time.deltaTime;
            }
            player.MovePosition(transform.position + tempVect);
        }
        else
        { 

        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Finish Line")
        {
            isFinish = true;
            GameManager.Instance.playerFinish[GameManager.Instance.countFinish] = playerNumber;
            GameManager.Instance.countFinish++;

        }

    }


}
