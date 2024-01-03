using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : SaiMonoBehaviour
{
    [Header("Enemy Movement")]
    public EnemySO enemyData;
    [SerializeField] protected Transform player;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBatEnemySO();
    }

    protected virtual void LoadBatEnemySO()
    {
        if (this.enemyData != null) return;
        string resPath = "Enemies/" + transform.name;
        this.enemyData = Resources.Load<EnemySO>(resPath);
        Debug.LogWarning(transform.name + ": LoadBatEnemySO", gameObject);
    }
    protected override void Start()
    {
        base.Start();
        this.player = FindObjectOfType<PlayerMovement>().transform;
    }

    protected override void Update()
    {
        base.Update();
        this.MovingFollowTarget();
    }

    protected virtual void MovingFollowTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.enemyData.MoveSpeed * Time.deltaTime);  
    }
}
