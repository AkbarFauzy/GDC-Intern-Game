using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int runningStageRound;
    public int[] playerFinish = new int[4];
    public int countFinish;
    public bool play;

    [System.Serializable]
    public struct rank
    {
        public int playerNumber;
        public int pos;
        public int overallPos;
        public float score;
        public float distanceToFinish;
        public string bufftype;
        public float playerEffect;
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

    public void EndGame() {
        play = false;
    }


}
