using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class weapon : MonoBehaviour
{

    private PlayersScript _player;
    public Transform firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    private int _damage = 10000;

    private IObjectPool<Bullet> _bulletPool;

    private bool _collectionCheck = false;
    private int _defaultCapacity = 20;
    private int _maxSize = 50;

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPoolBullet, 
            _collectionCheck, _defaultCapacity, _maxSize); 
    }

    void Start()
    {
        _player = GetComponent<PlayersScript>();
    }

    void Update()
    {
        if (GameManager.Instance.isPlay == true && !_player.IsFinish)
        {
            if (Input.GetKeyDown(_player.tapKey) && !_player.IsStuned)
            {
                Shoot();
            }
        }
    }

    public void Shoot() {
        Bullet bulletObject = _bulletPool.Get();
        bulletObject.gameObject.transform.position = firePoint.position;
        if (bulletObject == null)
            return;

        _damage = Random.Range(5000, 6001);
        /* if (_player.buffHolder.GetBuffType() == BuffType.BuffDamage || _player.buffHolder.GetBuffType() == BuffType.DebuffDamage)
         {
             damage = (int)(System.Math.Round((float)damage * _player.buffHolder.GetBuffValue()));
         }*/
        if (_player.PlayerLastStand())
        {
            _damage = (int)((float)_damage * 1.5f);
        }
        bulletObject.BulletDamage = _damage;
    }


    private void OnDestroyPoolBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(_bulletPrefab);
        bulletInstance.BulletPool = _bulletPool;
        bulletInstance.SetPlayerScript(_player);
        bulletInstance.SetBulletColor(_player.playerColor);
        return bulletInstance;
    }

}
