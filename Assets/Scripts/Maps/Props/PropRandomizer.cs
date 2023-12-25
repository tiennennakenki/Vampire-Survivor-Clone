using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : SaiMonoBehaviour
{
    [SerializeField] protected MapCtrl mapCtrl;
    public MapCtrl MapCtrl => mapCtrl;
    [SerializeField] protected List<GameObject> propSpawnPoints;
    public List<GameObject> PropSpawnPoints => propSpawnPoints;
    [SerializeField] protected List<GameObject> propPrefabs;
    public List<GameObject> PropPrefabs => propPrefabs;

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
        base.Start();
        this.SpawnProps();
    }

    protected virtual void SpawnProps()
    {
        foreach(GameObject child in this.propSpawnPoints)
        {
            int rand = Random.Range(0,this.propPrefabs.Count);

            GameObject obstacle = Instantiate(propPrefabs[rand].gameObject, child.transform.position, Quaternion.identity);
            obstacle.SetActive(true);

            obstacle.transform.parent = child.transform;
        }
    }
}
