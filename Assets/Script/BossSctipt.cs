using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSctipt : MonoBehaviour
{
    public int HP= 10000000;
    public int MaxHP;
    public HealthBarScript HpBar;
    public HealthPercentage HpPercentage;
    private int attTimerStart = 10;
    private int attTimerEnd = 20;
    private int attType;
    private int chance;
    private bool isAttacking;
    public List<GameObject> target;
    public List<PlayersScript> target_script;
    public int p1, p2, p3;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = HP;
        HpBar.setMaxHealth(MaxHP);
        HpPercentage.setHealthPercentage(MaxHP, HP);

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
        if (HP < (MaxHP - (MaxHP * 80 / 100))) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,0,189,255);
            attTimerStart = 5;
            attTimerEnd = 10;
        }

        if (HP < 0) {
            HP = 0;
            //Change scene
        }

    }


    public bool isDefeted() {
        if (HP <= 0) {
            return true;
        }
        return false;
    }

    private int calculateAttTypeChance(int initialChance) {
        if (initialChance <= 10) // 10% four target
            return 4;
        if (initialChance <= 35) // 25% three target
            return 3;
        if (initialChance <= 65) // 30% two target
            return 2;
        if (initialChance <= 100) // 35% one target
            return 1;
        return 0;
    }

    IEnumerator countToAtt(float attTimerStart, float attTimerEnd) {
        while (!isDefeted())
        {
            yield return new WaitForSeconds(Random.Range(attTimerStart, attTimerEnd));

            chance = Random.Range(0, 101);
            attType = calculateAttTypeChance(chance);
            bossAttack(attType);
        }
    }

    public void bossAttack(int attType) {
        if (attType == 1)
        {
            p1 = Random.Range(0, 4);
            StartCoroutine(AttAP(p1));
        } else if (attType == 2) 
        {
            p1 = Random.Range(0, 4);
            p2 = Random.Range(0, 4);
            while (p1 == p2)
            {
                p2 = Random.Range(0, 4);
            }
            attTwoP(p1, p2);
        } else if (attType == 3)
        {
            p1 = Random.Range(0, 4);
            p2 = Random.Range(0, 4);
            while (p1 == p2)
            {
                p2 = Random.Range(0, 4);
            }
            p3 = Random.Range(0,4);
            while (p3 == p1 || p3 == p2) {
                p3 = Random.Range(0, 4);
            }
            attThreeP(p1,p2,p3);
        }
        else if (attType == 4)
        {
            attAllP();
        }
    }

    IEnumerator AttAP(int pNumber) {
        isAttacking = true;
        for (int i = 0; i<10; i++) {
            target_script[pNumber].GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.2f);
            target_script[pNumber].GetComponent<SpriteRenderer>().color = target_script[pNumber].playerColor;
            yield return new WaitForSeconds(.2f);
        }

        target_script[pNumber].GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(1f);

        if (target[pNumber].GetComponent<weapon>().isAttacking)
        {
           target_script[pNumber].isStuned = true;
            target_script[pNumber].GetComponent<SpriteRenderer>().color = Color.grey;
            yield return new WaitForSeconds(3);
            target_script[pNumber].GetComponent<SpriteRenderer>().color = target_script[pNumber].playerColor;
            target_script[pNumber].isStuned = false;
        }

        target_script[pNumber].GetComponent<SpriteRenderer>().color = target_script[pNumber].playerColor;
        target_script[pNumber].isTargeted = false;
        isAttacking = false;
    }

        private void attTwoP(int pNumberOne, int pNumberTwo)
    {
        StartCoroutine(AttAP(pNumberOne));
        StartCoroutine(AttAP(pNumberTwo));
    }
    private void attThreeP(int pNumberOne, int pNumberTwo, int pNumberThree)
    {
        StartCoroutine(AttAP(pNumberOne));
        StartCoroutine(AttAP(pNumberTwo));
        StartCoroutine(AttAP(pNumberThree));
    }

    private void attAllP()
    {
        for (int i = 0; i < target.Count;i++)
        {
            StartCoroutine(AttAP(i));
        }
    } 

    public void applyDamage(int damageTaken, int player) {
        HP -= damageTaken;
        GameManager.Instance.playerRank[player - 1].score += damageTaken;
        HpBar.setHealth(HP);
        HpPercentage.setHealthPercentage(MaxHP, HP);
    }

    public void startDefeatedAnimtion() { 
        
    }
}
