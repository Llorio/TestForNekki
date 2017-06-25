using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorBase : MonoBehaviour {

    public int healthPoints_max;
    public float healthPoints { get; private set; }

    [SerializeField, Range(0f,1f)]public float defence;
    protected float _defence;

    public float speed_max;
    protected float _speed;

    [SerializeField]
    protected Weapon[] _weapons;
    protected int _currentWeapon = 0;

    [SerializeField]
    protected Rigidbody _actorRigidbody;
    [SerializeField]
    protected PoolObject _poolObject;
    protected UI_HealthInfoBar _uiHealthBar;

    public UnityAction<ActorBase> onActorDeath = (ActorBase _actor)=> { };
    public UnityAction<float> onActorGetDamage = (float _currHealth) => { };
    public virtual void Init(Vector3 _spawnPoint)
    {
        transform.position = _spawnPoint;

        _defence = defence;
        healthPoints = healthPoints_max;
        _speed = speed_max;

        _uiHealthBar = PoolManager.Current.getPoolObjectByName("healthBar") as UI_HealthInfoBar;
        _uiHealthBar.Init(this, GameManager.Current.UIGameCanvas.transform);
    }

    public void getGamage(float _damage)
    {
        healthPoints -= _damage * _defence;
        onActorGetDamage(healthPoints);
        if (healthPoints <= 0f)
        {
            PoolManager.Current.setPoolObjectBack(_poolObject);
            onActorDeath(this);
            PoolManager.Current.setPoolObjectBack(_uiHealthBar._poolObject);
        }
    }

}
