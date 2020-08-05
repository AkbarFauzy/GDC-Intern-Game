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
    public HealthBarScript HpBar;
    public Animator anim;
    public int playerNumber;
    public string charName;
    public int Hp;
    private int sceneIndex;
    public bool isFinish;
    public bool isStuned;
    public bool isDead;
    private bool isRunning;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        GameManager.Instance.playerRank[playerNumber - 1].tapKey = tapKey;
        GameManager.Instance.playerRank[playerNumber - 1].charName = charName;
        Hp = 1000; //GameManager.Instance.playerRank[playerNumber - 1].Hp;
        HpBar.setMaxHealth(Hp);
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

    private void FixedUpdate()
    {
        if (GameManager.Instance.play == true)
        {
           
            if (sceneIndex == 2)
            {
                RunningStage();
            }

            if (Hp <= 0)
            {
                playerIsDead();
                playerIsFinish();
            }
        }
    }

    void RunningStage() {
        if (!isFinish)
        {
            if (Input.GetKeyDown(tapKey))
            {
                speed += .1f;
            }
            else
            {
                if (speed > 0)
                {
                    speed -= .01f;
                    if (!isRunning) {
                        isRunning = true;
                        anim.SetBool("IsRunning", isRunning);
                    }
                }
                else {
                    speed = 0;
                    isRunning = false;
                    anim.SetBool("IsRunning", isRunning);
                }
            }
            Vector3 tempVect = new Vector3(1f, 0, 0);
           // if (GameManager.Instance.playerRank[playerNumber - 1].bufftype == "speed")
           // {
           //     tempVect = (tempVect.normalized * speed * Time.fixedDeltaTime) * GameManager.Instance.playerRank[playerNumber - 1].buffValue;
           // }
           // else {

          //  }
            player.MovePosition(player.position + tempVect * speed * Time.fixedDeltaTime);
        }
        else
        { 

        }
    }

    void playerIsFinish()
    {
        isFinish = true;
        GameManager.Instance.playerFinish[GameManager.Instance.countFinish] = playerNumber;
        GameManager.Instance.countFinish++;
    }

    public void playerIsDead() {
        GameManager.Instance.playerRank[playerNumber - 1].score = 0;
    }

    public bool playerLastStand() {
        return Hp <= 100;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Finish Line")
        {
            playerIsFinish();
           // Destroy(gameObject);

        }

    }

}
