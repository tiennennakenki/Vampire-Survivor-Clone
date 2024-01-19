using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : SaiMonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] protected EnemyStats enemy;
    [SerializeField] protected Transform player;

    [SerializeField] protected Vector2 knockbackVelocity;
    [SerializeField] protected float knockbackDuration;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadBatEnemySO();
    }

    //protected virtual void LoadBatEnemySO()
    //{
    //    if (this.enemyData != null) return;
    //    string resPath = "Enemies/" + transform.name;
    //    this.enemyData = Resources.Load<EnemySO>(resPath);
    //    Debug.LogWarning(transform.name + ": LoadBatEnemySO", gameObject);
    //}
    protected override void Start()
    {
        base.Start();
        this.player = FindObjectOfType<PlayerMovement>().transform;
        this.enemy = GetComponent<EnemyStats>();
    }

    protected override void Update()
    {
        base.Update();
        this.MovingFollowTarget();
        this.Knockbacking();
    }

    protected virtual void MovingFollowTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.enemy.currentMoveSpeed * Time.deltaTime);  
    }

    public virtual void Knockback(Vector2 velocity, float duration)
    {
        if (this.knockbackDuration > 0) return;

        //Begin the knockback
        this.knockbackVelocity = velocity;
        this.knockbackDuration = duration;
    }

    protected virtual void Knockbacking()
    {
        if(this.knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        }
    }
}
