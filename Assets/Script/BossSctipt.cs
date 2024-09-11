using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSctipt : MonoBehaviour
{
    public int HP = GameManager.BOSSMAXHEALTH;
    public HealthBarScript HpBar;
    public HealthPercentage HpPercentage;
    public GameObject attackAnimPrefab;
    public Animator damagedAnim;
    public Animator bossAnim;
    public Transform firePoint;
    public float attackWindup;

    private int _attTimerStart = 1;
    private int _attTimerEnd = 5;

    public List<GameObject> target;
    public List<PlayersScript> target_script;

    public bool IsDefeated { get => HP <= 0; }

    // Start is called before the first frame update
    void Start()
    {
        HpBar.SetMaxHealthBoss(GameManager.BOSSMAXHEALTH);
        HpPercentage.SetHealthPercentage(GameManager.BOSSMAXHEALTH, HP);
        for (int i = 0; i < target.Count; i++)
        {
            target_script.Add(target[i].GetComponent<PlayersScript>()); 
        }
        StartCoroutine(CountToAtt(_attTimerStart, _attTimerEnd));
    }

    // Update is called once per frame
    void Update()
    {
        if (isBerserk()) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,0,189,255);
           // attTimerStart = 5;
           // attTimerEnd = 10;
        }

        if (IsDefeated) {
            HP = 0;
            StartDefeatedAnimation();
        }
    }

    public bool isBerserk()
    {
        return HP < (GameManager.BOSSMAXHEALTH - (GameManager.BOSSMAXHEALTH * 80 / 100));
    }

    IEnumerator CountToAtt(float attTimerStart, float attTimerEnd) {
        yield return new WaitForSeconds(7f);
        while (!IsDefeated)
        {
            attackWindup = Random.Range(0.2f, 1f);
            OnAttackAllPlayer();
            yield return new WaitForSeconds(Random.Range(attTimerStart, attTimerEnd) + 4f);
        }
    }

    private void OnAttackAllPlayer()
    {
        for (int i = 0; i < target.Count; i++)
        {
            StartCoroutine(AttackPlayer(i));
        }
    }

    IEnumerator AttackPlayer(int pNumber) {
         //isAttacking = true;
        //int ran = Random.Range(5, 10);

        Vector3 alignPos = firePoint.position;

        if (!target_script[pNumber].IsFinish)
        {
            alignPos.z = target[pNumber].GetComponent<Transform>().position.z;
            attackAnimPrefab.GetComponent<BossBullet>().target = target[pNumber].GetComponent<Transform>();
            attackAnimPrefab.GetComponent<BossBullet>().bossStatus = isBerserk();
            attackAnimPrefab.GetComponent<BossBullet>().windupTime = attackWindup;
            Instantiate(attackAnimPrefab, alignPos, firePoint.rotation);

            yield return new WaitForSeconds(0.5f);

/*            if (target[pNumber].GetComponent<weapon>().isAttacking)
            {
                //target_script[pNumber].isStuned = true;
*//*                target[pNumber].GetComponent<SpriteRenderer>().color = Color.grey;
                target[pNumber].GetComponent<SpriteRenderer>().color = target_script[pNumber].playerColor;*//*
                //target_script[pNumber].isStuned = false;
            }*/
            // isAttacking = false;
        }
    }

    public void ApplyDamage(int damageTaken, PlayersScript player) {
        damagedAnim.SetTrigger("damaged");
        HP -= damageTaken;
        player.AddScore(damageTaken);
        HpBar.SetHealth(HP);
        HpPercentage.SetHealthPercentage(GameManager.BOSSMAXHEALTH, HP);
    }

      
    public void StartDefeatedAnimation() {
        bossAnim.SetBool("isDefeated", IsDefeated);
    }

    public void NextLevelTrigger() {
        StartCoroutine(LevelLoaderScript.Instance.LoadLevel(5));
    }

}
