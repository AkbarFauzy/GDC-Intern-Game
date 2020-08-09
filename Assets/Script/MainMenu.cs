using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerReady",LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
