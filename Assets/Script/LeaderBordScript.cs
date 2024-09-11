using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBordScript : MonoBehaviour
{
    private int scoreCounter;
    private PlayersScript[] _players;

    [SerializeField] private List<SpriteRenderer> characterSprites;
    [SerializeField] private List<Text> _positionText;
    [SerializeField] private List<Text> _scoreText;

    private void Awake()
    {
        _players = GameManager.Instance.PlayerFinish;  
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SetPodium();
            return;
        }

        Animator anim = gameObject.GetComponent<Animator>();
        bool isAllDead = true;
        for (int i = 0; i < 4; i++)
        {
            if (!_players[i].IsDead)
            {
                isAllDead = false;
                break;
            }
        }

        if (isAllDead)
        {
           anim.SetBool("GO",true);
        }
        else
        {
            anim.SetBool("GO", false);
        }

        scoreCounter = 200000;

        for (int i=0;i<_positionText.Count;i++) {
            _positionText[i].text = _players[i].playerNumber.ToString();

            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                StartCoroutine(CountUpToTarget(_scoreText[i], _players[i].StageScore, _players[i].StageScore, 2.0f));
            }
            else {
                StartCoroutine(CountUpToTarget(_scoreText[i], scoreCounter + _players[i].StageScore, _players[i].StageScore, 2.0f));
                _players[i].StageScore += scoreCounter;
            }
            
            characterSprites[i].color = _players[i].playerColor;
            GameManager.Instance.SavePlayerScore(_players[i]);

            scoreCounter -= 50000;
        }

       /* GameManager.Instance.ChangePosByScore(_players[0].score, _players[1].score, _players[2].score, _players[3].score);*/

    }


    IEnumerator CountUpToTarget(Text label, float targetVal, float current,float duration, float delay = 5.0f, string prefix = "")
    {
        label.text = prefix + current;
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        while (current < targetVal)
        {
            int currentNonZero = (int)(targetVal / (duration / Time.deltaTime));
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

    private void SetPodium() {
        var playerScores = GameManager.Instance.PolePositions;
        var sortedScores = playerScores.OrderByDescending(player => player.Value.PlayerScore).ToList();

        for (int i = 0; i < sortedScores.Count; i++) {
            _scoreText[i].text = sortedScores[i].Value.PlayerScore.ToString();
            switch (sortedScores[i].Value.PlayerNumber)
            {
                case 1:
                    characterSprites[i].color = new Color32(0, 115, 255, 255);
                    break;
                case 2:
                    characterSprites[i].color = new Color32(219, 163, 0, 255);
                    break;
                case 3:
                    characterSprites[i].color = new Color32(255,0,0,255);
                    break;
                case 4:
                    characterSprites[i].color = new Color32(0,171,24,255);
                    break;
            }
        }
    }


}
