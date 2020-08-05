using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBordScript : MonoBehaviour
{
    private int scoreCounter;

    public List<SpriteRenderer> sp;
    public List<Text> pos;
    public List<Text> score;

    private void Start()
    {
        scoreCounter = 200000;

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
            scoreCounter -= 50000;
        }

        GameManager.Instance.ChangePosByScore(GameManager.Instance.playerRank[0].score, GameManager.Instance.playerRank[1].score, GameManager.Instance.playerRank[2].score, GameManager.Instance.playerRank[3].score);

    }


    IEnumerator CountUpToTarget(Text label, float targetVal, float current,float duration, float delay = 5.0f, string prefix = "")
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
