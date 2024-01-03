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
        //For override
    }
}
