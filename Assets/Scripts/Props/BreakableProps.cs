using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableProps : SaiMonoBehaviour
{
    public ObstacleSO obstacleData;
    [SerializeField] protected float health = 10;
    public float Health => health;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadObstacleData();
    }

    //protected virtual void LoadObstacleData()
    //{
    //    if (this.obstacleData != null) return;
    //    string resPath = "Obstacles/Obstacle";
    //    this.obstacleData = Resources.Load<ObstacleSO>(resPath);
    //    Debug.LogWarning(transform.name + ": LoadObstacleData", gameObject);
    //}

    public virtual void TakeDamage(float dmg)
    {
        this.health -= dmg;

        if(this.health <= 0)
        {
            this.Kill();
        }
    }

    protected virtual void Kill()
    {
        this.OnDeadDrop();
        Destroy(gameObject);
    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemsDropSpawner.Instance.Drop(this.obstacleData.dropList, dropPos, dropRot);
    }
}
