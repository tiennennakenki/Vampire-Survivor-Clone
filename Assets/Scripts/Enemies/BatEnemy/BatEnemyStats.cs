using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemyStats : EnemyStats
{
    protected override void LoadEnemySO()
    {
        if (this.enemyData != null) return;
        string resPath = "Enemies/" + transform.name;
        this.enemyData = Resources.Load<EnemySO>(resPath);
        Debug.LogWarning(transform.name + ": LoadBatEnemySO", gameObject);
    }

    protected override void OnDead()
    {
        this.OnDeadDrop();
        BatEnemySpawner.Instance.Despawn(transform.parent);
    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemsDropSpawner.Instance.Drop(this.enemyData.dropList, dropPos, dropRot);
    }
}
