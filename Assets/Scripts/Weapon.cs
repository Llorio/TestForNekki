using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon {

    public Transform _weaponTransform;

    public string bulletName; //название пули в PoolManager-е

    public float fireRate;

    private Coroutine _shootCoroutinePointer;

    private bool _shooting = false;
    public bool shooting {
        set
        {
            _shooting = value;
            if (value&&_shootCoroutinePointer==null)
            {
                _shootCoroutinePointer = GameManager.Current.StartCoroutine(shootCoroutine());
            }
        }
        get { return _shooting; }
    }

    IEnumerator shootCoroutine()
    {
        Bullet _newBullet = PoolManager.Current.getPoolObjectByName(bulletName) as Bullet;
        _newBullet.Init(_weaponTransform.position,_weaponTransform.forward);
        yield return new WaitForSeconds(fireRate);
        if (_shooting)
        {
            _shootCoroutinePointer = GameManager.Current.StartCoroutine(shootCoroutine());
        }
        else
        {
            _shootCoroutinePointer = null;
        }
    }

}
