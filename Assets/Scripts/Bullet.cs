using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    [SerializeField] PoolObject _poolObject;
    // public Vector3 direction { get; private set; }
    private float timer = 0f;
    public void Init (Vector3 _startPosition,Vector3 _direction) {
        //direction = _direction;
        transform.position = _startPosition;
        transform.forward = _direction;
        timer = 0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        timer += Time.deltaTime;
        if(timer>_lifetime) PoolManager.Current.setPoolObjectBack(_poolObject);

        transform.position += transform.forward * _speed;

        Ray _ray = new Ray(transform.position, transform.forward);
        RaycastHit _raycastHit;
        if (Physics.SphereCast(_ray,1f, out _raycastHit, _speed))
        {
            if (_raycastHit.transform.gameObject.CompareTag("Enemy"))
            {
                _raycastHit.transform.GetComponent<ActorBase>().getGamage(_damage);
            }
            
           Explosion _explosion = PoolManager.Current.getRandomPoolObjectByType(PoolObjectType.Explosion) as Explosion;
           _explosion.Init(transform.position);
            PoolManager.Current.setPoolObjectBack(_poolObject);
        }

       
    }
}
