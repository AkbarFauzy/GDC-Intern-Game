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

    public IEnumerator LoadLevel(int lvlIndex) 
    {
        transition.SetTrigger("start");
        /*GameManager.Instance.countFinish = 0;*/
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("end");
        SceneManager.LoadScene(lvlIndex);
        ChangeBGMTheme(lvlIndex);
    }

    public void ChangeBGMTheme(int lvlIndex) {
        FindObjectOfType<AudioManager>().StopSound();
        if (lvlIndex == 0) {
            FindObjectOfType<AudioManager>().PlaySound("Menu_BGM");
        } else if (lvlIndex == 2) {
            FindObjectOfType<AudioManager>().PlaySound("Running_BGM");
        } else if (lvlIndex == 4) {
            FindObjectOfType<AudioManager>().PlaySound("Boss_BGM");
        }
        
    }
}
