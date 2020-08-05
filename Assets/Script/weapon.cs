using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

    private PlayersScript player;
    private bool isPlayerStuned;
    public bool isAttacking;
    public bool targetIsDefeated;
    public float upTime, downTime;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject damagePopUpPrefab;
    public GameObject boss;
    public int damage = 10000;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayersScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.play == true && !player.isFinish)
        {
            isPlayerStuned = player.isStuned;
            targetIsDefeated = boss.GetComponent<BossSctipt>().isDefeted();
            if (Input.GetKeyDown(player.tapKey) && !isPlayerStuned && !targetIsDefeated)
            {
                upTime = Time.time;
                shoot();
                isAttacking = true;
            }
            else
            {
                downTime = Time.time;
            }

            if (downTime - upTime >= 0.5f)
            {
                isAttacking = false;
                upTime = 0f;
                downTime = 0f;
            }
        }
    }

    public void shoot() {
        damage = Random.Range(5000, 6000);
        if (player.playerLastStand()) { 
            damage = (int)((float)damage * 1.5f);
        }
        bulletPrefab.GetComponent<bullet>().bulletDamage = damage;
        bulletPrefab.GetComponent<bullet>().bulletID = GetComponent<PlayersScript>().playerNumber;
        bulletPrefab.GetComponent<bullet>().bulletColor = GetComponent<PlayersScript>().playerColor;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        damagePopUpPrefab.GetComponent<PopUpDamageScript>().playerColor = GetComponent<PlayersScript>().playerColor;
        damagePopUpPrefab.GetComponent<PopUpDamageScript>().damageValue = damage;
    }

}
