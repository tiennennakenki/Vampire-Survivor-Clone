using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : SaiMonoBehaviour
{
    public MapSpawner mapSpawner;

    public GameObject targetMap;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapSpawner();
        this.LoadTargetMap();
    }

    protected virtual void LoadMapSpawner()
    {
        if (this.mapSpawner != null) return;
        this.mapSpawner = FindObjectOfType<MapSpawner>();
        Debug.LogWarning(transform.name + ": LoadMapSpawner", gameObject);
    }

    protected virtual void LoadTargetMap()
    {
        if (this.targetMap != null) return;
        this.targetMap = transform.parent.GetComponent<BoxCollider2D>().gameObject;
        Debug.LogWarning(transform.name + ": LoadTargetMap", gameObject);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mapSpawner.currentChunk = targetMap;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (mapSpawner.currentChunk == targetMap)
            {
                mapSpawner.currentChunk = null;
            }
        }
    }
}
