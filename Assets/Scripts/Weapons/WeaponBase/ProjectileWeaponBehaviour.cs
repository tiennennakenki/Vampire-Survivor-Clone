using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeaponBehaviour : SaiMonoBehaviour
{
    [Header("Projectile Weapon Behaviour")]
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

    public virtual float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
    }

    public virtual void DirectionChecker(Vector3 dir)
    {
        this.direction = dir;

        float dirX = this.direction.x;
        float dirY = this.direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirX < 0 && dirY == 0) //left
        {
            rotation.z = 135f;
        }
        else if (dirX > 0 && dirY == 0) //right
        {
            rotation.z = -45f;
        }
        else if(dirX == 0 && dirY < 0) //down
        {
            rotation.z = -135f;
        }
        else if (dirX == 0 && dirY > 0) //up
        {
            rotation.z = 45f;
        }
        else if (dirX > 0 && dirY > 0) //right up
        {
            rotation.z = 0f;
        }
        else if (dirX > 0 && dirY < 0) //right down
        {
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY > 0) //left up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY < 0) //left down
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(GetCurrentDamage());
            ReducePierce();
        }
        else if(collision.gameObject.CompareTag("Prop"))
        {
            if(collision.gameObject.TryGetComponent(out BreakableProps breakableProps))
            {
                breakableProps.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    protected virtual void ReducePierce()
    {
        this.currentPierce--;
        if (this.currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
