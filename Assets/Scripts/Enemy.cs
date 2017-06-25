using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : ActorBase {
     [SerializeField]private NavMeshAgent _navMeshAgent;
     [SerializeField]private float collisionDamage;

    public override void Init(Vector3 _spawnPoint)
    {
        base.Init(_spawnPoint);

        _navMeshAgent.speed = _speed;
        transform.position = _spawnPoint;
        StartCoroutine(updateDestination());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().getGamage(collisionDamage);

            getGamage(float.MaxValue);

            Explosion _explosion = PoolManager.Current.getRandomPoolObjectByType(PoolObjectType.Explosion) as Explosion;
            _explosion.Init(transform.position);
        }
    }

    private IEnumerator updateDestination()
    {
        _navMeshAgent.destination = GameManager.Current.player.transform.position;
        yield return new WaitForSeconds(2f);
        StartCoroutine(updateDestination());
    }


}
