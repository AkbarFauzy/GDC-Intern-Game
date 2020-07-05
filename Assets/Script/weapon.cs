using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

    private KeyCode playerKey;
    private bool isPlayerStuned;
    public bool isAttacking;
    public bool targetIsDefeated;
    public float upTime, downTime;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject damagePopUpPrefab;
    public GameObject boss;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        playerKey = GetComponent<PlayersScript>().tapKey;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerStuned = GetComponent<PlayersScript>().isStuned;
        targetIsDefeated = boss.GetComponent<BossSctipt>().isDefeted();
        if (Input.GetKeyDown(playerKey) && !isPlayerStuned && !targetIsDefeated )
        {  
            upTime = Time.time;
            shoot();
            isAttacking = true;
        }
        else {
            downTime = Time.time;
        }

        if (downTime - upTime >= 0.5f)
        {
            isAttacking = false;
            upTime = 0f;
            downTime = 0f;
        }

    }

    public void shoot() {
        damage = Random.Range(10000, 15000);
        bulletPrefab.GetComponent<bullet>().bulletDamage = damage;
        bulletPrefab.GetComponent<bullet>().bulletID = GetComponent<PlayersScript>().playerNumber;
        bulletPrefab.GetComponent<bullet>().bulletColor = GetComponent<PlayersScript>().playerColor;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        damagePopUpPrefab.GetComponent<PopUpDamageScript>().playerColor = GetComponent<PlayersScript>().playerColor;
        damagePopUpPrefab.GetComponent<PopUpDamageScript>().damageValue = damage;
    }

}
