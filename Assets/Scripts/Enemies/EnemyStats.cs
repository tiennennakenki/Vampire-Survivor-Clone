using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : SaiMonoBehaviour
{
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
        this.LoadBatEnemySO();
    }

    protected virtual void LoadBatEnemySO()
    {
        if (this.enemyData != null) return;
        string resPath = "Enemy/" + transform.name;
        this.enemyData = Resources.Load<EnemySO>(resPath);
        Debug.LogWarning(transform.name + ": LoadBatEnemySO", gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        this.currentHealth -= damage;
        if(this.currentHealth <= 0 )
        {
            this.Kill();
        }
    }

    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
}
