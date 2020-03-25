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
    //private Vector2 movement;
    private bool isFinish;
    private int sceneIndex;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        //movement = new Vector2(0, 0);
    }

    private void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 1) {
            RunningStage();
        } else if (sceneIndex == 2) {

        } else if (sceneIndex == 3) { 
        
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        
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
            Debug.LogWarning("You won");
        }
    }

    void OnTriggerEnter(Collider coll) {
        isFinish = true;
    }



}
