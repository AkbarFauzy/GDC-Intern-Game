using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScript : MonoBehaviour
{
    public Transform finishLine;
    public List<Transform> target;
    public List<SpriteRenderer> playerBoard;
    
    [System.Serializable]
    struct rank {
        public int playerNumber;
        public int pos;
        public float score;
        public float distanceToFinish;
    }

    private rank[] playerRank = new rank[4];

    private void Start()
    {
        for (int i = 0;i<target.Count;i++) {
            playerRank[i].playerNumber = i+1;
            playerRank[i].pos = 1;
            playerRank[i].distanceToFinish = finishLine.position.x - target[i].position.x;
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
            playerRank[i].distanceToFinish = finishLine.position.x - target[i].position.x;
        }
    }

    void CountPos() {
        for (int i = 0; i < target.Count; i++)
        {
            int pos = 0;
            int j = 0;
            while (j < target.Count)
            {
                if (playerRank[j].distanceToFinish <= playerRank[i].distanceToFinish)
                    pos++;
                j++;
            }
            playerRank[i].pos = pos;
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
