using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSpawner : SaiMonoBehaviour
{
    [SerializeField] protected MapCtrl mapCtrl;
    public MapCtrl MapCtrl => mapCtrl;
    public List<GameObject> terrainChunks;
    [SerializeField] protected GameObject player;
    [SerializeField] protected float checkerRadius = .2f;
    public Vector3 noTerrainPosition;
    [SerializeField] protected LayerMask terrainMask;
    public GameObject currentChunk;
    [SerializeField] protected PlayerMovement playerMovement;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist = 50; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapCtrl();
        this.LoadPlayer();
        this.LoadPlayerMovement();
        this.LoadTerrainChunks();
        this.LoadTerrainMask();
    }
    protected virtual void LoadMapCtrl()
    {
        if (this.mapCtrl != null) return;
        this.mapCtrl = transform.parent.GetComponent<MapCtrl>();
        Debug.LogWarning(transform.name + ": LoadMapCtrl", gameObject);
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.Find("Player");
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = this.player.GetComponentInChildren<PlayerMovement>();
        Debug.LogWarning(transform.name + ": LoadPlayerMovement", gameObject);
    }

    protected virtual void LoadTerrainChunks()
    {
        if (this.terrainChunks.Count > 0) return;
        this.terrainChunks = new List<GameObject>();

        Transform propsTransform = this.mapCtrl.TerrainChunk.transform;
        foreach (Transform child in propsTransform)
        {
            this.terrainChunks.Add(child.gameObject);
        }
        Debug.LogWarning(transform.name + ": LoadTerrainChunks", gameObject);
    }

    protected virtual void LoadTerrainMask()
    {
        this.terrainMask = 1 << LayerMask.NameToLayer("Terrain");
    }

    protected override void Update()
    {
        this.ChunkChecker();
        this.ChunkOptimzer();
    }

    protected virtual void ChunkChecker()
    {
        if (!currentChunk)
        {
            Debug.Log("Current Chunk = null");
            return;
        }

        if (playerMovement.direction.x > 0 && playerMovement.direction.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;  //Right
                SpawnChunk();
            }
        }
        else if (playerMovement.direction.x < 0 && playerMovement.direction.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;    //Left
                SpawnChunk();
            }
        }
        else if (playerMovement.direction.y > 0 && playerMovement.direction.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position; //Up
                SpawnChunk();
            }
        }
        else if (playerMovement.direction.y < 0 && playerMovement.direction.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;    //Down
                SpawnChunk();
            }
        }
        else if (playerMovement.direction.x > 0 && playerMovement.direction.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;   //Right up
                SpawnChunk();
            }
        }
        else if (playerMovement.direction.x > 0 && playerMovement.direction.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;  //Right down
                SpawnChunk();
            }
        }
        else if (playerMovement.direction.x < 0 && playerMovement.direction.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;  //Left up
                SpawnChunk();
            }
        }
        else if (playerMovement.direction.x < 0 && playerMovement.direction.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position; //Left down
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        latestChunk.SetActive(true);
        spawnedChunks.Add(latestChunk);
        latestChunk.transform.SetParent(transform);
    }

    void ChunkOptimzer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;   //Check every 1 second to save cost, change this value to lower to check more times
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
