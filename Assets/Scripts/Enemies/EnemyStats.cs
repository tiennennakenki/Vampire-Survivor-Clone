using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : SaiMonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemySO enemyData;

    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;

    protected override void Awake()
    {
        base.Awake();
        this.currentMoveSpeed = this.enemyData.MoveSpeed;
        this.currentHealth = this.enemyData.MaxHealth;
        this.currentDamage = this.enemyData.Damage;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySO();
    }

    protected virtual void LoadEnemySO()
    {
        //For override
    }

    public virtual void TakeDamage(float damage)
    {
        this.currentHealth -= damage;
        if(this.currentHealth <= 0 )
        {
            this.OnDead();
        }
    }

    protected virtual void OnDead()
    {
        this.OnDeadDrop();
        EnemiesSpawner.Instance.Despawn(transform);
        EnemiesSpawner.Instance.OnEnemyKilled();
    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = this.transform.position;
        Quaternion dropRot = this.transform.rotation;
        ItemsDropSpawner.Instance.Drop(this.enemyData.dropList, dropPos, dropRot);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    public virtual void ResetCurrentHealth()
    {
        this.currentHealth = this.enemyData.MaxHealth;
    }
}
