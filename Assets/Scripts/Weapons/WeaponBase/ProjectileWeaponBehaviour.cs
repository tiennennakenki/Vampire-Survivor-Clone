using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : SaiMonoBehaviour
{
    public WeaponSO weaponData;
    [SerializeField] protected Vector3 direction;

    //Current stats
    [SerializeField] protected float currentDamage;
    [SerializeField] public float currentSpeed;
    [SerializeField] protected float currentCoolDownDuration;
    [SerializeField] protected float currentPierce;

    protected override void Awake()
    {
        base.Awake();
        this.currentDamage = this.weaponData.Damage;
        this.currentSpeed = this.weaponData.Speed;
        this.currentCoolDownDuration = this.weaponData.CoolDownDuration;
        this.currentPierce = this.weaponData.Pierce;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(currentDamage);
        }
    }
}
