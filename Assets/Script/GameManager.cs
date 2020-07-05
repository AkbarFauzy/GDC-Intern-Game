using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int runningStageRound;
    public int[] playerFinish = new int[4];
    public int[] overal = new int[4];
    public int countFinish;
    public bool play;

    [System.Serializable]
    public struct rank
    {
        public int playerNumber;
        public int pos;
        public int overallPos;
        public int score;
        public float distanceToFinish;
        public string bufftype;
        public float buffValue;
        public string charName;
        public KeyCode tapKey;
    }

    public rank[] playerRank = new rank[4];

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

    public void BeginGame()
    {
        play = true;
    }

    public void resetBuff() {
        for (int i=0;i<GameManager.Instance.playerRank.Length;i++)
        {
            GameManager.Instance.playerRank[i].bufftype = "";
            GameManager.Instance.playerRank[i].buffValue = 1;
        }
    
    }

    public void EndGame() {
        play = false;
    }

    public void ChangePosByScore(int s1, int s2, int s3, int s4)
    {
        overal[0] = s1;
        overal[1] = s2;
        overal[2] = s3;
        overal[3] = s4;

        Array.Sort(overal);
        Array.Reverse(overal);
        int counterPos = 1;
        for (int i = 0; i < 4; i++)
        {
            int j = 0;
            while (GameManager.Instance.playerRank[j].score != overal[i])
            {
                j++;
            }
            GameManager.Instance.playerRank[j].overallPos = counterPos;
            if (i < 3)
            {
                while (j < 3 && i < 3 && overal[i] == overal[i + 1])
                {
                    j += 1;
                    GameManager.Instance.playerRank[j].overallPos = counterPos;
                    if (i < 3)
                    {
                        i++;
                    }
                }
            }
            counterPos++;
        }

    }

}
