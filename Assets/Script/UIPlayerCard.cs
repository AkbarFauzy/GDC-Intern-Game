using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlayerCard : MonoBehaviour
{
    
    [SerializeField] private Text _playerScore;
    [SerializeField] private Text _playerBoard;
    [SerializeField] private Image _playerEffect;

    private void Start()
    {
/*        players.playerNumber = i+1;
        players[i].position = 1;*/
        /*targetEffect(player.buffHolder.GetBuffType());*/
    }

    private void Update()
    {

      /* if (SceneManager.GetActiveScene().buildIndex == 4){
            GameManager.Instance.ChangePosByScore(GameManager.Instance.playerRank[0].score, GameManager.Instance.playerRank[1].score, GameManager.Instance.playerRank[2].score, GameManager.Instance.playerRank[3].score);
            for (int i = 0; i < player.Count; i++)
            {
                ChangePosText(GameManager.Instance.playerRank[i].overallPos, i);
                playerScore[i].text = GameManager.Instance.playerRank[i].score.ToString();
            }
        }*/
        
    }

    public void ChangePosText(int pos) {
        if (pos == 0) {
            _playerBoard.text = "R.I.P";
        }else if (pos == 1) {
            _playerBoard.text = "1";
        } else if(pos == 2){
            _playerBoard.text = "2";
        } else if(pos == 3){
            _playerBoard.text = "3";
        } else if(pos == 4){
            _playerBoard.text = "4";
        }
    }

    public void SetEffectImage(BuffType type) {
        switch (type) {
            case BuffType.BuffDamage:
                _playerEffect.sprite = GameManager.Instance.BuffSprite[0];
                _playerEffect.color = new Color32(112, 243, 255, 255);
                break;
            case BuffType.DebuffDamage:
                _playerEffect.sprite = GameManager.Instance.BuffSprite[0];
                _playerEffect.color = new Color32(238, 0, 0, 255);
                break;
            case BuffType.BuffSpeed:
                _playerEffect.sprite = GameManager.Instance.BuffSprite[1];
                _playerEffect.color = new Color32(112, 243, 255, 255);
                break;
            case BuffType.DebuffSpeed:
                _playerEffect.sprite = GameManager.Instance.BuffSprite[2];
                _playerEffect.color = new Color32(238, 0, 0, 255);
                break;
            case BuffType.BuffHealth:
                _playerEffect.sprite = GameManager.Instance.BuffSprite[3];
                break;
            case BuffType.Life:
                _playerEffect.sprite = GameManager.Instance.BuffSprite[3];
                _playerEffect.color = new Color32(171, 255, 0, 255);
                break;
            default:
                break;
        }
    }

    public void SetScoreText(string value)
    {
        _playerScore.text = value;  
    }

    public void SetPositionText(string pos) {
        _playerBoard.text = pos;
    }
}
