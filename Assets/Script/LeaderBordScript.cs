using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LeaderBordScript : MonoBehaviour
{
    private float scoreCounter;

    public List<SpriteRenderer> sp;
    public List<Text> pos;
    public List<Text> score;

    private float[] overal = new float[4];

    private void Start()
    {
        scoreCounter = 200000f;

        for (int i=0;i<pos.Count;i++) {
            pos[i].text = GameManager.Instance.playerRank[GameManager.Instance.playerFinish[i] - 1].charName;
            StartCoroutine(CountUpToTarget(score[i], scoreCounter + GameManager.Instance.playerRank[GameManager.Instance.playerFinish[i] - 1].score, GameManager.Instance.playerRank[GameManager.Instance.playerFinish[i] - 1].score, 2.0f));
            GameManager.Instance.playerRank[GameManager.Instance.playerFinish[i] - 1].score += scoreCounter;
            if (GameManager.Instance.playerFinish[i] == 1){
                sp[i].color = new Color32(0, 115, 255,255);
            } else if (GameManager.Instance.playerFinish[i] == 2){
                sp[i].color = new Color32(219, 193, 0, 255);
            } else if (GameManager.Instance.playerFinish[i] == 3){
                sp[i].color = new Color32(255, 0, 0, 255);
            } else if (GameManager.Instance.playerFinish[i] == 4){
                sp[i].color = new Color32(0, 171, 24, 255);
            }
            scoreCounter -= 50000f;
        }

        OveralPos(GameManager.Instance.playerRank[0].score, GameManager.Instance.playerRank[1].score, GameManager.Instance.playerRank[2].score, GameManager.Instance.playerRank[3].score);

    }

    private void OveralPos(float s1, float s2, float s3, float s4)
    {
        overal[0] = s1;
        overal[1] = s2;
        overal[2] = s3;
        overal[3] = s4;

        Array.Sort(overal);
        Array.Reverse(overal);
        int counterPos = 1;
        for (int i = 0;i<4;i++) {
            int j = 0;
            while (GameManager.Instance.playerRank[j].score != overal[i]) {
                j++;
            }
            GameManager.Instance.playerRank[j].overallPos = counterPos;
            if (i < 3)
            {
                while (j < 3 && i < 3 && overal[i] == overal[i + 1])
                {
                    j += 1;
                    GameManager.Instance.playerRank[j].overallPos = counterPos;
                    if (i<3) {
                        i++;
                    }
                }
            }
            counterPos++;
        }

    }

    IEnumerator CountUpToTarget(Text label, float targetVal, float current,float duration, float delay = 1.0f, string prefix = "")
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        int currentNonZero = 1;
        while (current < targetVal)
        {
            currentNonZero = (int)(targetVal / (duration / Time.deltaTime));
            if (currentNonZero == 0)
            {
                currentNonZero = 1;
            }  
            current += currentNonZero;
            current = Mathf.Clamp(current, 0, targetVal);
            label.text = prefix + current;
            yield return null;
        }
    }


}
