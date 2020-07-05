using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public Rigidbody bulletRb;
    public GameObject target;
    public int bulletDamage;
    public int bulletID;
    public Color32 bulletColor;
    public GameObject damagePopUpPrefab;

    private void Start()
    {
        target = GameObject.Find("Boss");
        gameObject.GetComponent<SpriteRenderer>().color = bulletColor;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        BossSctipt boss = other.gameObject.GetComponent<BossSctipt>();

        if (other.gameObject.name == "Boss")
        {
            boss.applyDamage(bulletDamage, bulletID);
            Instantiate(damagePopUpPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }



}
  