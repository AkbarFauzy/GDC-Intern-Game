using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public static LevelLoaderScript Instance { get; private set; }

    public float transitionTime;
    public Animator transition;
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
                showLB = true;
            }
            LoadNextLevel();      
        }
    }

    public void LoadNextLevel()
    {
        if (GameManager.Instance.runningStageRound == 0)
        {
            GameManager.Instance.resetBuff();
        }

        if (GameManager.Instance.runningStageRound < 2) {
            if (showLB) {
                StartCoroutine(LoadLevel(5));
                GameManager.Instance.runningStageRound++;
            }
            else {
                if (SceneManager.GetActiveScene().buildIndex < 3) {
                    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
                }
                else if (SceneManager.GetActiveScene().buildIndex == 3) {
                    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
                }
                else if (SceneManager.GetActiveScene().buildIndex == 5) {
                    StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 2));
                }
            }
        }
        else {
            GameManager.Instance.runningStageRound = 0;
            StartCoroutine(LoadLevel(0));
        }
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
