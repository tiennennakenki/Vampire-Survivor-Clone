using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCtrl : SaiMonoBehaviour
{
    [SerializeField] protected GameObject terrainChunk;
    public GameObject TerrainChunk => terrainChunk;

    [SerializeField] protected GameObject obstacle;
    public GameObject Obstacle => obstacle;
    [SerializeField] protected GameObject mapSpawner;
    public GameObject MapSpawner => mapSpawner;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTerrainChunk();
        this.LoadObstacle();
        this.LoadMapSpawner();
    }

    protected virtual void LoadTerrainChunk()
    {
        if (this.terrainChunk != null) return;
        this.terrainChunk = transform.Find("Terrain Chunk").gameObject;
        Debug.LogWarning(transform.name + ": LoadTerrainChunk", gameObject);
    }

    protected virtual void LoadObstacle()
    {
        if (this.obstacle != null) return;
        this.obstacle = transform.Find("Obstacle").gameObject;
        Debug.LogWarning(transform.name + ": LoadObstacle", gameObject);
    }

    protected virtual void LoadMapSpawner()
    {
        if (this.mapSpawner != null) return;
        this.mapSpawner = transform.Find("MapSpawner").gameObject;
        Debug.LogWarning(transform.name + ": LoadMapSpawner", gameObject);
    }
}
