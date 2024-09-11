using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public const int BOSSMAXHEALTH = 10000000;
    private int stageRound;

    //Dictionary <PlayerNumber, PlayerScore>
    [SerializedDictionary("Position", "Player Score")]
    public SerializedDictionary<int, PolePosition> PolePositions;

    public bool isPlay;
    public int countFinish;
    private bool isShowLeaderBoard;

    public PlayersScript[] PlayerFinish = new PlayersScript[4];
    public GameObject leaderBoardPrefab;

    public List<Sprite> BuffSprite;

    [Serializable]
    public struct PolePosition{
        public int PlayerNumber;
        public int PlayerPosition;
        public int PlayerScore;
        public BuffType BuffType;
        public float BuffValue;

        public PolePosition(int playerNumber,int position, int playerScore, BuffType buffType = BuffType.None, float buffValue = 0) {
            PlayerNumber = playerNumber;
            PlayerPosition = position;
            PlayerScore = playerScore;
            BuffType = buffType;
            BuffValue = buffValue;
        }
    }

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

    void LateUpdate()
    {
        if (isPlay && SceneManager.GetActiveScene().buildIndex == 1)
        {
            isPlay = false;
            stageRound = 0;
            LoadNextLevel();
        }

        if (isPlay && SceneManager.GetActiveScene().buildIndex == 5 && Input.GetKeyDown("space"))
        {
            isPlay = false;
            stageRound = 0;
            StartCoroutine(LevelLoaderScript.Instance.LoadLevel(0));
        }

        if (isShowLeaderBoard && Input.GetKeyDown("space"))
        {
            isShowLeaderBoard = false;
            LoadNextLevel();
        }
        else if (countFinish == 4)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (isShowLeaderBoard == false)
                {
                    stageRound += 1;
                    StartCoroutine(LeaderBoard());
                }
                isShowLeaderBoard = true;
            }
            else
            {
                LoadNextLevel();
            }
        }
    }

    public void LoadNextLevel()
    {
        if (stageRound == 0)
        {
            ResetGame();
            StartCoroutine(LevelLoaderScript.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        if (stageRound < 2)
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 3:
                    StartCoroutine(LevelLoaderScript.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
                    break;
                default:
                    StartCoroutine(LevelLoaderScript.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
                    break;
            }
        }
        else if (stageRound == 2)
        {
            StartCoroutine(LevelLoaderScript.Instance.LoadLevel(4));
            stageRound += 1;
        }
        else
        {
            StartCoroutine(LevelLoaderScript.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        countFinish = 0;
    }

    public void BeginGame()
    {
        isPlay = true;
/*        PlayerFinish = new PlayersScript[4];*/
    }

    public void SavePlayerFinish(PlayersScript player) {
        PlayerFinish[countFinish] = player;
    }

    public void SavePlayerScore(PlayersScript player)
    {
        var Player = PolePositions[player.playerNumber];
        Player.PlayerScore = player.StageScore;
        Player.PlayerPosition = player.Position;
        PolePositions[player.playerNumber] = Player;
    }

    public void SavePlayerBuff(PlayersScript player, BuffType buffType, float buffValue) {
        var Player = PolePositions[player.playerNumber];
        Player.BuffType = buffType;
        Player.BuffValue = buffValue;
        PolePositions[player.playerNumber] = Player;
        player.PlayerCard.SetEffectImage(buffType);
    }

    public void ResetScore()
    {
        PolePositions = new SerializedDictionary<int, PolePosition> {
            { 1, new PolePosition(1, 4 ,0) },
            { 2, new PolePosition(2, 4 ,0) },
            { 3, new PolePosition(3, 4,0) },
            { 4, new PolePosition(4, 4,0) },
        };
    }

    public void EndGame() {
        isPlay = false;
    }

    public void ResetGame()
    {
        stageRound = 0;
/*        resetBuff();*/
        ResetScore();
       /* resetHp();*/
    }

    IEnumerator LeaderBoard()
    {
        yield return new WaitForSeconds(2f);
        GameObject Lb = Instantiate(leaderBoardPrefab, transform.position, Quaternion.identity) as GameObject;
        Lb.GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
