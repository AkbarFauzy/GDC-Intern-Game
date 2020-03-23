using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScript : MonoBehaviour
{
    public List<Transform> target;
    public Transform finishLine;
    
    [System.Serializable]
    struct rank {
        public int playerNumber;
        public int pos;
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

        for (int i = 0; i < target.Count; i++) {
            int pos = 1;
            int j = 0;
            while (j<target.Count-1) {
                if (i == j)
                    j++;
                if (playerRank[j].distanceToFinish < playerRank[i].distanceToFinish)
                    pos++;
                j++;
            }
            playerRank[i].pos = pos;
        }

    }

    void DistanceToFinish() {
        for (int i = 0; i < target.Count; i++)
        {
            playerRank[i].distanceToFinish = finishLine.position.x - target[i].position.x;
        }
    }

    

}
