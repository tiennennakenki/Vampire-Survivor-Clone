using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    protected List<GameObject> markedEnemies; 
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGarlicSO();
    }
    protected virtual void LoadGarlicSO()
    {
        if (this.weaponData != null) return;
        string resPath = "Weapons/Garlic Weapon/" + transform.name;
        this.weaponData = Resources.Load<WeaponSO>(resPath);
        Debug.Log(resPath);
        Debug.LogWarning(transform.name + ": LoadGarlicSO", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.markedEnemies = new List<GameObject>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !this.markedEnemies.Contains(collision.gameObject))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(GetCurrentDamage(), transform.position);

            this.markedEnemies.Add(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Prop"))
        {
            if (collision.gameObject.TryGetComponent(out BreakableProps breakableProps) && !this.markedEnemies.Contains(collision.gameObject))
            {
                breakableProps.TakeDamage(GetCurrentDamage());
                this.markedEnemies.Add(collision.gameObject);
            }
        }
    }
}
