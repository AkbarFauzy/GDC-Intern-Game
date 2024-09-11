using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private PlayersScript _player;
    private float _bulletSpeed = 10f;
    private float _lifeTime = 5f;

    public int BulletDamage;

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _outputPosition;
    [SerializeField] private Color32 _bulletColor;
    [SerializeField] private GameObject _damagePopUpPrefab;

    private PopUpDamageScript _popUpInstance;

    public IObjectPool<Bullet> BulletPool { private get; set; }

    private void Start()
    {
        var PopUp = Instantiate(_damagePopUpPrefab, _outputPosition.transform.position, Quaternion.identity);
        _popUpInstance = PopUp.GetComponent<PopUpDamageScript>();
        _popUpInstance.SetDamageValue(BulletDamage);
        _popUpInstance.SetColor(_bulletColor);
        _popUpInstance.gameObject.SetActive(false);

        gameObject.GetComponent<SpriteRenderer>().color = _bulletColor;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        BossSctipt boss = other.gameObject.GetComponent<BossSctipt>();

        if (other.gameObject.name == "Boss" && !other.GetComponent<BossSctipt>().IsDefeated)
        {
            boss.ApplyDamage(BulletDamage, _player);
            Deactivate();
            _popUpInstance.gameObject.transform.position = _outputPosition.transform.position;
            _popUpInstance.gameObject.SetActive(true);
        }
    }

    public void SetPlayerScript(PlayersScript player)
    {
        _player = player;
    }

    public void SetBulletColor(Color color)
    {
        _bulletColor = color;
    }

    public void Deactivate(float delay = 0f)
    {
        StartCoroutine(DeactivateRoutine(delay)); ;
    }

    private IEnumerator DeactivateRoutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        BulletPool.Release(this);
    }
}
