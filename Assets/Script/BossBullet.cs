using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Transform target;
    public AnimationEvent windup;
    public float windupTime;
    public bool bossStatus;

    private float _bulletSpeed = 10f;
    private int _damage = 100;
    private Animator _bulletAnim;
    private bool _windupTrigger;
    private Vector3 x;


    public void Start()
    {
        _bulletAnim = gameObject.GetComponent<Animator>();
        _bulletAnim.SetBool("isBerserk",bossStatus);
        _bulletAnim.SetFloat("windupDuration", windupTime);
        x = new Vector3(-100f, target.position.y, target.position.z);
        windup.floatParameter = windupTime;
        StartCoroutine(WindUP(1/windupTime));
    }

    public void Update()
    {
        if (_windupTrigger)
        {
            transform.position = Vector3.MoveTowards(transform.position, x, _bulletSpeed * Time.deltaTime);
        }
    }

    IEnumerator WindUP(float duration)
    {
        yield return new WaitForSeconds(duration);
        _windupTrigger = true;
    }

    IEnumerator TimeUntilDestroy() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private int CalculateAttackDamage(PlayersScript player)
    {
        int initDamage = _damage;
        if (bossStatus)
        {
            initDamage *= 2;
        }

        switch (player.Position)
        {
            case 1:
                initDamage += ((int)((float)initDamage * 0.75f));
                break;
            case 2:
                initDamage += ((int)((float)initDamage * 0.5f));
                break;
            case 3:
                initDamage += ((int)((float)initDamage * 0.25f));
                break;
            default:
                break;
        }

        return initDamage;
    }
    private int CalculateStunDuration(PlayersScript player)
    {
        int duration;

        switch (player.Position)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            PlayersScript player = other.gameObject.GetComponent<PlayersScript>();
            float debuffDuration = CalculateStunDuration(player);

            if (player.IsAttacking)
            {
                player.TakeDamage(CalculateAttackDamage(player));
                player.OnStun(debuffDuration);
                _bulletAnim.SetBool("isHit", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player") {
            StartCoroutine(TimeUntilDestroy());
        }
    }

}
