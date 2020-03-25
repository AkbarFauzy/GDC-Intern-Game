using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if (false) {
            LoadNextLevel();
        }      
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+ 1));
    }

    IEnumerator LoadLevel(int lvlIndex) {

        
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(lvlIndex);
    }

}
