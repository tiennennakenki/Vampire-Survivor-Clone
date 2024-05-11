using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : SaiMonoBehaviour
{
    [SerializeField] protected MapCtrl mapCtrl;
    public MapCtrl MapCtrl => mapCtrl;
    public List<GameObject> terrainChunks;
    [SerializeField] protected PlayerCtrl player;
    [SerializeField] protected float checkerRadius = .2f;
    public Vector3 noTerrainPosition;
    [SerializeField] protected LayerMask terrainMask;
    public GameObject currentChunk;
    public Vector3 playerLastPosition;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist = 50; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur = 1;
    private HashSet<Vector3> spawnedChunkPositions = new HashSet<Vector3>();
    private bool hasSpawnedChunkThisFrame = false;


    protected override void OnEnable()
    {
        PlayerSelection.CharacterSetEvent += LoadPlayer;
        if (player != null)
        {
            this.playerLastPosition = this.player.transform.position;
        }
        else
        {
            Debug.LogWarning("Player is null. Make sure to assign the player object in the inspector.");
        }
    }


    protected override void OnDisable()
    {
        PlayerSelection.CharacterSetEvent -= LoadPlayer;
    }

    #region LoadComponent
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapCtrl();
        //this.LoadPlayerMovement();
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

        //this.player = PlayerCtrl.Instance.GetComponent<PlayerCtrl>();
        this.player = PlayerCtrl.Instance;
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
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
    #endregion

    protected override void Update()
    {
        this.ChunkChecker();
        this.ChunkOptimzer();
    }

    protected virtual void ChunkChecker()
    {
        if (!currentChunk) return;

        Vector3 moveDir = this.player.transform.position - this.playerLastPosition;
        this.playerLastPosition = this.player.transform.position;

        string directionName = this.GetDirectionName(moveDir);
        //this.CheckAndSpawnChunk(directionName);

        if (!hasSpawnedChunkThisFrame)
        {
            if (directionName.Contains("Up") && !HasSpawnedChunkAtPosition(currentChunk.transform.Find("Up").position))
            {
                this.CheckAndSpawnChunk("Up");
                this.CheckAndSpawnChunk("Right Up");
                this.CheckAndSpawnChunk("Left Up");
            }
            if (directionName.Contains("Down") && !HasSpawnedChunkAtPosition(currentChunk.transform.Find("Down").position))
            {
                this.CheckAndSpawnChunk("Down");
                this.CheckAndSpawnChunk("Right Down");
                this.CheckAndSpawnChunk("Left Down");
            }
            if (directionName.Contains("Right") && !HasSpawnedChunkAtPosition(currentChunk.transform.Find("Right").position))
            {
                this.CheckAndSpawnChunk("Right");
                this.CheckAndSpawnChunk("Right Up");
                this.CheckAndSpawnChunk("Right Down");
            }
            if (directionName.Contains("Left") && !HasSpawnedChunkAtPosition(currentChunk.transform.Find("Left").position))
            {
                this.CheckAndSpawnChunk("Left");
                this.CheckAndSpawnChunk("Left Up");
                this.CheckAndSpawnChunk("Left Down");
            }
        }

        // Reset the flag at the end of the frame
        hasSpawnedChunkThisFrame = false;
    }

    protected virtual void CheckAndSpawnChunk(string directionName)
    {
        if (Physics2D.OverlapCircle(this.currentChunk.transform.Find(directionName).position, this.checkerRadius, this.terrainMask))
            return;
        SpawnChunk(currentChunk.transform.Find(directionName).position);
        hasSpawnedChunkThisFrame = true; // Mark that a chunk has been spawned
    }


    protected virtual string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            //Moving horizontally more than the vertically
            if(direction.y > -0.5f)
            {
                //Also moving upwards
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if (direction.y < -0.5f)
            {
                //Also moving downwards
                return direction.x < 0 ? "Right Down" : "Left Down";
            }
            else
            {
                //Moving straight horizontally
                return direction.x < 0 ? "Right" : "Left";
            }
        }
        else
        {
            //Moving vertically more than the horizontally
            if (direction.x > -0.5f)
            {
                //Also moving right
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                //Also moving left
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                //Moving straight vertically
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        latestChunk.SetActive(true);
        spawnedChunks.Add(latestChunk);
        latestChunk.transform.SetParent(transform);

        spawnedChunkPositions.Add(spawnPosition);
    }

    // Check if a chunk has been spawned at the given position
    bool HasSpawnedChunkAtPosition(Vector3 position)
    {
        return spawnedChunkPositions.Contains(position);
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
