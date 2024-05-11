using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PropRandomizer : SaiMonoBehaviour
{
    [SerializeField] protected MapCtrl mapCtrl;
    public MapCtrl MapCtrl => mapCtrl;
    [SerializeField] protected List<GameObject> propSpawnPoints;
    public List<GameObject> PropSpawnPoints => propSpawnPoints;
    [SerializeField] protected List<GameObject> propPrefabs;
    public List<GameObject> PropPrefabs => propPrefabs;

    private HashSet<Vector3> spawnedPositions = new HashSet<Vector3>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapCtrl();
        this.LoadProps();
        this.LoadObstacle();
    }

    protected virtual void LoadMapCtrl()
    {
        if (this.mapCtrl != null) return;
        this.mapCtrl = transform.parent.parent.GetComponent<MapCtrl>();
        Debug.LogWarning(transform.name + ": LoadMapCtrl", gameObject);
    }

    protected virtual void LoadProps()
    {
        if (this.propSpawnPoints.Count > 0) return;
        this.propSpawnPoints = new List<GameObject>();

        Transform propsTransform = transform.Find("Props");
        foreach(Transform child in propsTransform)
        {
            this.propSpawnPoints.Add(child.gameObject);
        }
        Debug.LogWarning(transform.name + ": LoadProps", gameObject);
    }

    protected virtual void LoadObstacle()
    {
        if (this.propPrefabs.Count > 0) return;

        this.propPrefabs = new List<GameObject>();
        Transform prefabsTransform = this.mapCtrl.Obstacle.transform;

        foreach(Transform child in prefabsTransform)
        {
            this.propPrefabs.Add(child.gameObject); 
        }
        Debug.LogWarning(transform.name + ": LoadObstacle", gameObject);
    }

    protected override void Start()
    {
        this.SpawnProps();
    }

    bool HasSpawnedAtPosition(Vector3 position)
    {
        return spawnedPositions.Contains(position);
    }

    protected virtual void SpawnProps()
    {
        foreach(GameObject child in this.propSpawnPoints)
        {
            if (HasSpawnedAtPosition(child.transform.position)) continue;
            int rand = Random.Range(0,this.propPrefabs.Count);

            GameObject obstacle = Instantiate(propPrefabs[rand].gameObject, child.transform.position, Quaternion.identity);
            obstacle.SetActive(true);

            obstacle.transform.parent = child.transform;
            spawnedPositions.Add(child.transform.position);
        }
    }
}
