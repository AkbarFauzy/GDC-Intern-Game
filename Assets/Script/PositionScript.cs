using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PositionScript : MonoBehaviour
{
    public Transform finishLine;
    public List<GameObject> target;
    public List<Text> playerScore;
    public List<Text> playerBoard;

    private void Start()
    {
        for (int i = 0;i<target.Count;i++) {
            GameManager.Instance.playerRank[i].playerNumber = i+1;
            GameManager.Instance.playerRank[i].pos = 1;
            GameManager.Instance.playerRank[i].distanceToFinish = finishLine.position.x - target[i].GetComponent<Transform>().position.x;
            playerScore[i].text = GameManager.Instance.playerRank[i].score.ToString();
        }

    }

    private void Update()
    {
       if (SceneManager.GetActiveScene().buildIndex == 2) {
            DistanceToFinish();
            CountPos();
       }

       /* if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            for (int i = 0; i < target.Count; i++)
            {
                ChangePosText(GameManager.Instance.playerRank[i].overallPos, i);
                playerScore[i].text = GameManager.Instance.playerRank[i].score.ToString();
            }
        }*/

       if (SceneManager.GetActiveScene().buildIndex == 4){
            GameManager.Instance.ChangePosByScore(GameManager.Instance.playerRank[0].score, GameManager.Instance.playerRank[1].score, GameManager.Instance.playerRank[2].score, GameManager.Instance.playerRank[3].score);
            for (int i = 0; i < target.Count; i++)
            {
                ChangePosText(GameManager.Instance.playerRank[i].overallPos, i);
                playerScore[i].text = GameManager.Instance.playerRank[i].score.ToString();
            }
        }
        
    }

    void DistanceToFinish() {
        for (int i = 0; i < target.Count; i++)
        {
            GameManager.Instance.playerRank[i].distanceToFinish = finishLine.position.x - target[i].GetComponent<Transform>().position.x;
        }
    }

    void CountPos() {
        for (int i = 0; i < target.Count; i++)
        {
            int pos = 0;
            int j = 0;
            while (j < target.Count)
            {
                if (GameManager.Instance.playerRank[j].distanceToFinish <= GameManager.Instance.playerRank[i].distanceToFinish)
                    pos++;
                j++;
            }
            GameManager.Instance.playerRank[i].pos = pos;
            ChangePosText(pos, i);
        }
    }

    void ChangePosText(int pos, int player) {
        if (target[player].GetComponent<PlayersScript>().Hp <= 0) {
            playerBoard[player].text = "R.I.P";
        }
        else if (pos == 1) {
            playerBoard[player].text = "1";
        } else if(pos == 2){
            playerBoard[player].text = "2";
        } else if(pos == 3){
            playerBoard[player].text = "3";
        } else if(pos == 4){
            playerBoard[player].text = "4";
        }
    }

}
