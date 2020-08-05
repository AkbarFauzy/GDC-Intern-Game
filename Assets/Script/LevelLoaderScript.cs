using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelLoaderScript : MonoBehaviour
{
    public static LevelLoaderScript Instance { get; private set; }

    public float transitionTime;
    public Animator transition;
    public GameObject leaderBoardPrefab;
    private bool showLB;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transitionTime = 3f;
    }

    void LateUpdate()
    {
        if (GameManager.Instance.play && SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameManager.Instance.play = false;
            LoadNextLevel();
        }
        if (showLB && Input.GetKeyDown("space"))
        {
            showLB = false;
            LoadNextLevel();
        }

        if (GameManager.Instance.countFinish == 4) {
            if (SceneManager.GetActiveScene().buildIndex == 2) {
                if (showLB == false)
                {
                    StartCoroutine(LeaderBoard());
                }
                showLB = true;
                
            }      
        }
    }

    public void LoadNextLevel()
    {
        if (GameManager.Instance.runningStageRound == 0)
        {
            GameManager.Instance.resetBuff();
        }

        if (GameManager.Instance.runningStageRound < 2)
        {
            switch (SceneManager.GetActiveScene().buildIndex) {
                case 3:
                    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
                    break;
                default:
                    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
                    break;
            }
        }
        else if (GameManager.Instance.runningStageRound == 2)
        {
            StartCoroutine(LoadLevel(4));
        }
        else {
            StartCoroutine(LoadLevel(0));
        }
    }

    IEnumerator LeaderBoard()
    {
        yield return new WaitForSeconds(2f);
        leaderBoardPrefab.GetComponent<Canvas>().worldCamera = Camera.main;
        Instantiate(leaderBoardPrefab, leaderBoardPrefab.transform);
    }

    IEnumerator LoadLevel(int lvlIndex) 
    {
        transition.SetTrigger("start");
        GameManager.Instance.countFinish = 0;
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("end");
        SceneManager.LoadScene(lvlIndex);
    }

}
