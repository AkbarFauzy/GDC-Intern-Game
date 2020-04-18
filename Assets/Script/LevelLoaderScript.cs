using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public float transitionTime = 1f;
    public Animator transition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.countFinish == 4) {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        if (GameManager.Instance.runningStageRound < 2) {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            GameManager.Instance.runningStageRound++;
        } else if (GameManager.Instance.runningStageRound == 2) {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
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

        SceneManager.LoadScene(lvlIndex);
    }

}
