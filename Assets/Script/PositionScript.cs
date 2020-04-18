using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScript : MonoBehaviour
{
    public Transform finishLine;
    public List<Transform> target;
    public List<SpriteRenderer> playerBoard;

    private void Start()
    {
        for (int i = 0;i<target.Count;i++) {
            GameManager.Instance.playerRank[i].playerNumber = i+1;
            GameManager.Instance.playerRank[i].pos = 1;
            GameManager.Instance.playerRank[i].distanceToFinish = finishLine.position.x - target[i].position.x;
        }

    }

    private void Update()
    {
        DistanceToFinish();
        CountPos();
    }

    void DistanceToFinish() {
        for (int i = 0; i < target.Count; i++)
        {
            GameManager.Instance.playerRank[i].distanceToFinish = finishLine.position.x - target[i].position.x;
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
            ChangePos(pos, i);
        }
    }

    void ChangePos(int pos, int player) {
        if (pos == 1) {
            playerBoard[player].color = Color.cyan;
        } else if(pos == 2){
            playerBoard[player].color = Color.yellow;
        } else if(pos == 3){
            playerBoard[player].color = Color.green;
        } else if(pos == 4){
            playerBoard[player].color = Color.blue;
        }
    }

}
