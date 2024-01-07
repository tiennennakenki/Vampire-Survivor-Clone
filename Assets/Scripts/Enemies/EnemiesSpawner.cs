using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : Spawner
{
    [Header("Enemies Spawner")]
    private static EnemiesSpawner instance;
    public static EnemiesSpawner Instance => instance;
    public List<Wave> waves = new List<Wave>();
    public int currentWaveCount = 0;
    [SerializeField] protected PlayerStats player;

    [Header("Spawner Attributes")]
    [SerializeField] protected float spawnTimer; //Timer use to determine when to spawn next enemy
    [SerializeField] protected int enemiesAlive;
    [SerializeField] protected int maxEnemiesAllowed = 4; //The maximum number of enemies allowed on the map at once
    [SerializeField] protected bool maxEnemiesReached; //A flag indicating if the maximum number of enemies had been reached
    [SerializeField] protected float waveInterval; //The Interval bettwen each wave

    [Header("Spawn Point")]
    [SerializeField] protected List<Transform> spawnPoints;

    protected override void Awake()
    {
        base.Awake();
        if (EnemiesSpawner.instance != null) Debug.Log("Only 1 EnemiesSpawner allow to exits");
        EnemiesSpawner.instance = this;
        player = FindObjectOfType<PlayerStats>();
    }

    protected override void Start()
    {
        base.Start();
        player= FindObjectOfType<PlayerStats>();
        this.CaculatorWaveQuote();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPoints();
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.spawnPoints.Count > 0) return;
        Transform listSpawnPoint = transform.Find("SpawnPoints");
        foreach(Transform point in listSpawnPoint)
        {
            this.spawnPoints.Add(point);
        }
        Debug.LogWarning(transform.name + ": LoadSpawnPoints", gameObject);
    }

    protected override void Update()
    {
        base.Update();
        if(this.currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
        this.SpawnEnemy();
    }

    IEnumerator BeginNextWave()
    {
        //Wave for 'waveInterval' seconds before the starting next wave
        yield return new WaitForSeconds(this.waveInterval);

        //If there are more wave to start after the current wave, move on to the next wave
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            this.CaculatorWaveQuote();
        }
    }

    protected virtual void CaculatorWaveQuote()
    {
        int currentWaveQuote = 0;
        foreach(var enemy in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuote += enemy.enemyCount;
        }

        waves[currentWaveCount].waveQuote = currentWaveQuote;
    }

    protected override void Spawn()
    {
        base.Spawn();
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuote && !this.maxEnemiesReached)
        {
            foreach (var enemy in waves[currentWaveCount].enemyGroups)
            {
                if (enemy.spawnCount < enemy.enemyCount)
                {
                    if(this.enemiesAlive >= this.maxEnemiesAllowed)
                    {
                        this.maxEnemiesReached = true;
                        return;
                    }

                    Vector2 spawnPosition = this.player.transform.position + spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                    Transform newEnemy = this.GetObjectFromPool(enemy.enemyPrefabs.transform); //Create enemy

                    newEnemy.transform.position = spawnPosition; // Set position for the enemy
                    newEnemy.transform.SetParent(this.holder);
                    newEnemy.gameObject.SetActive(true);

                    enemy.spawnCount++;
                    this.waves[currentWaveCount].spawnCount++;
                    this.enemiesAlive++;
                }
            }
        }
        //Reset the maxEnemiesAllowed flag if the number of enemies alive has dropped bellow the maximum amount
        if (this.enemiesAlive < this.maxEnemiesAllowed)
        {
            this.maxEnemiesReached = false;
        }
    }

    protected virtual void SpawnEnemy()
    {
        this.spawnTimer += Time.deltaTime;

        if(this.spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            this.spawnTimer = 0;
            this.Spawn();
        }
    }

    public virtual void OnEnemyKilled()
    {
        this.enemiesAlive--;
    }
}
