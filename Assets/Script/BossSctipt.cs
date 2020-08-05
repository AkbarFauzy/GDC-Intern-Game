using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSctipt : MonoBehaviour
{
    public int HP = GameManager.bossMaxHealth;
    public int damage = 100;
    public HealthBarScript HpBar;
    public HealthPercentage HpPercentage;
    private int attTimerStart = 5;
    private int attTimerEnd = 10;

    public List<GameObject> target;
    public List<PlayersScript> target_script;
    public int p1, p2, p3;
    private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        HpBar.setMaxHealth(GameManager.bossMaxHealth);
        HpPercentage.setHealthPercentage(GameManager.bossMaxHealth, HP);

        isAttacking = false;
        for (int i = 0; i < target.Count; i++)
        {
            target_script.Add(target[i].GetComponent<PlayersScript>()); 
        }
        StartCoroutine(countToAtt(attTimerStart, attTimerEnd));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBerserk()) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,0,189,255);
           // attTimerStart = 5;
           // attTimerEnd = 10;
        }

        if (HP <= 0) {
            HP = 0;
            GameManager.Instance.countFinish = 4;
            GameManager.Instance.runningStageRound++;
        }

    }


    public bool isDefeted() {
        if (HP <= 0) {
            return true;
        }
        return false;
    }

    public bool isBerserk()
    {
        return HP < (GameManager.bossMaxHealth - (GameManager.bossMaxHealth * 80 / 100));
    }

    private int calculateAttDamage(int pNumber) {
        int initDamage = damage;
        if (isBerserk())
        {
            initDamage *= 2;
        }

        switch (GameManager.Instance.playerRank[pNumber].overallPos)
        {
            case 1:
                initDamage += ((int)((float)damage * 0.75f));
                break;
            case 2:
                initDamage += ((int)((float)damage * 0.5f));
                break;
            case 3:
                initDamage += ((int)((float)damage * 0.25f));
                break;
            default:
                break;
        }

        return initDamage;
    }

    private int calculateStunDuration(int pNumber)
    {
        int duration;

        switch (GameManager.Instance.playerRank[pNumber].overallPos)
        {
            case 1:
                duration = 4;
                break;
            case 2:
                duration = 3;
                break;
            case 3:
                duration = 2;
                break;
            default:
                duration = 1;
                break;
        }

        return duration;
    }

    IEnumerator countToAtt(float attTimerStart, float attTimerEnd) {
        yield return new WaitForSeconds(Random.Range(attTimerStart, attTimerEnd));
        while (!isDefeted())
        {
            attAllP();
            yield return new WaitForSeconds(Random.Range(attTimerStart, attTimerEnd));
        }
    }

    private void attAllP()
    {
        for (int i = 0; i < target.Count; i++)
        {
            StartCoroutine(AttAP(i));
        }
    }

    IEnumerator AttAP(int pNumber) {
        isAttacking = true;
        int ran = Random.Range(5, 10);
        if (!target_script[pNumber].isFinish)
        {
            for (int i = 0; i < 10; i++) {
                target[pNumber].GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(.2f);
                target[pNumber].GetComponent<SpriteRenderer>().color = target_script[pNumber].playerColor;
                yield return new WaitForSeconds(.2f);
            }

            target_script[pNumber].GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.5f);

            if (target[pNumber].GetComponent<weapon>().isAttacking)
            {
                target_script[pNumber].isStuned = true;
                target[pNumber].GetComponent<SpriteRenderer>().color = Color.grey;
                target_script[pNumber].Hp -= calculateAttDamage(pNumber);
                target_script[pNumber].HpBar.setHealth(target_script[pNumber].Hp);
                yield return new WaitForSeconds(calculateStunDuration(pNumber));

                target[pNumber].GetComponent<SpriteRenderer>().color = target_script[pNumber].playerColor;
                target_script[pNumber].isStuned = false;
            }

            target[pNumber].GetComponent<SpriteRenderer>().color = target_script[pNumber].playerColor;
            isAttacking = false;
        }
    }


    public void applyDamage(int damageTaken, int player) {
        HP -= damageTaken;
        GameManager.Instance.playerRank[player - 1].score += damageTaken;
        HpBar.setHealth(HP);
        HpPercentage.setHealthPercentage(GameManager.bossMaxHealth, HP);
    }

    public void startDefeatedAnimtion() { 
        
    }
}
