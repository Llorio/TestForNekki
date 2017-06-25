using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager> {

    public bool gamePlaying = false;

    public Player player;

    [SerializeField]private Transform[] _spawnPoints;
    [SerializeField]private Transform _playerSpawnPoint;
    [SerializeField]private UI_HealthInfoBar ui_healthBarPrefab;
    [SerializeField]public GameObject UIGameCanvas;
    [SerializeField]private GameObject UIMenuCanvas;

 
    public int maxEnemies = 10;
    public float spawnRate = 3f;
    [SerializeField]
    private int _enemiesCount = 0;

    private List<Enemy> _enemiesList = new List<Enemy>();
    public void startGame () {
        UIMenuCanvas.SetActive(false);
        UIGameCanvas.SetActive(true);


        gamePlaying = true;
        player = PoolManager.Current.getPoolObjectByName("player") as Player;
        player.Init( _playerSpawnPoint.position);
        player.onActorDeath += onPlayerDeath;
        StartCoroutine(enemiesSpawner());

    }
    private IEnumerator enemiesSpawner()
    {
        spawnEnemy(_spawnPoints[Random.Range(0, _spawnPoints.Length)]);
        yield return new WaitForSeconds(spawnRate);
        while (_enemiesCount >= maxEnemies)
        {
            yield return new WaitForEndOfFrame();
        }
        if (gamePlaying) StartCoroutine(enemiesSpawner());
    }

    private void spawnEnemy(Transform _spawnPoint)
    {
       Enemy _newEnemy = PoolManager.Current.getRandomPoolObjectByType(PoolObjectType.Enemy) as Enemy;
        _newEnemy.Init(_spawnPoint.position);
        _enemiesCount++;
        _enemiesList.Add(_newEnemy);
        _newEnemy.onActorDeath += onEnemyDeath;
    }

    private void onEnemyDeath(ActorBase _actor)
    {
        _enemiesCount--;
        _enemiesList.Remove(_actor as Enemy);
    }

    private void onPlayerDeath(ActorBase _actor)
    {
        gamePlaying = false;
        UIMenuCanvas.SetActive(true);
        UIGameCanvas.SetActive(false);
    }
}
