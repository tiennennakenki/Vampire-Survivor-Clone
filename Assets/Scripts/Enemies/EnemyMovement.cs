using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : SaiMonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] protected EnemyStats enemyStats;
    [SerializeField] protected Transform player;

    [SerializeField] protected Vector2 knockbackVelocity;
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected List<Transform> spawnPointsClone;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyStats();
    }

    protected override void Start()
    {
        this.player = PlayerCtrl.Instance.PlayerMovement.transform;
        spawnPointsClone = EnemiesSpawner.Instance.spawnPoints;
    }

    protected virtual void LoadEnemyStats()
    {
        if (this.enemyStats != null) return;

        this.enemyStats = this.GetComponent<EnemyStats>();
    }
    protected override void Update()
    {
        this.MovingFollowTarget();
        this.Knockbacking();
        this.MoveEnemyIntoCamera(spawnPointsClone);
    }

    protected virtual void MovingFollowTarget()
    {
        Vector3 previousTransform = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.enemyStats.currentMoveSpeed * Time.deltaTime);

        //transform.LookAt(player.transform);
        if(transform.position.x > previousTransform.x)
        {
            transform.localScale = new Vector3(
               Mathf.Abs(transform.localScale.x),
               transform.localScale.y,
               transform.localScale.z
           );
        }
        else
        {
            transform.localScale = new Vector3(
              -Mathf.Abs(transform.localScale.x),
               transform.localScale.y,
               transform.localScale.z
            );
        }
    }

    protected virtual void MoveEnemyIntoCamera(List<Transform> spawnPointsClone)
    {
        Vector2 movePosition = spawnPointsClone[Random.Range(0, spawnPointsClone.Count)].transform.position;
        
        if(Vector3.Distance(this.transform.position, this.player.position) > 25f)
        {
            this.transform.position = movePosition;
        }
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
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.currentMoveSpeed * Time.deltaTime);
        }
    }
}
