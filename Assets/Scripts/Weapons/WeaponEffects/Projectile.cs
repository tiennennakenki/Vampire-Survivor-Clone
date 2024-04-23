using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : WeaponEffect
{
    public enum DamageSource { projectile, owner };
    public DamageSource damageSource = DamageSource.projectile;
    public bool hasAutoAim = false;
    public Vector3 rotationSpeed = new Vector3(0, 0, 0);

    protected Rigidbody2D rb;
    protected int piercing;

    protected Weapon.Stats stats;

    protected override void Start()
    {
        GetInfo();
    }
    protected override void OnEnable()
    {
        GetInfo();
    }

    protected virtual void GetInfo()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) return;

        stats = weapon != null ? weapon.GetStats() : new Weapon.Stats(); // Kiểm tra null cho weapon
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.angularVelocity = rotationSpeed.z;
            rb.velocity = transform.right * stats.speed * (weapon != null ? weapon.Owner.Stats.speed : 1f); // Kiểm tra null cho weapon và weapon.Owner
        }

        float area = weapon != null ? weapon.GetArea() : 1f; // Kiểm tra null cho weapon
        if (area <= 0) area = 1;
        transform.localScale = new Vector3(
            area * Mathf.Sign(transform.localScale.x),
            area * Mathf.Sign(transform.localScale.y), 1
        );

        piercing = stats.piercing;

        if (stats.lifespan > 0 && !IsInvoking("DespawnWeapon")) // Kiểm tra xem đã gọi Invoke hay chưa
        {
            Invoke("DespawnWeapon", stats.lifespan);
        }

        if (hasAutoAim) AcquireAutoAimFacing();
    }


    protected virtual void DespawnWeapon()
    {
        WeaponSpawner.Instance.Despawn(gameObject.transform);
    }

    public virtual void AcquireAutoAimFacing()
    {
        float aimAngle;
        EnemyStats[] targets = FindObjectsOfType<EnemyStats>();
        if (targets.Length > 0)
        {
            EnemyStats selectedTarget = targets[Random.Range(0, targets.Length)];
            Vector2 difference = selectedTarget.transform.position - transform.position;
            aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else
        {
            aimAngle = Random.Range(0f, 360f);
        }

        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    protected override void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            Weapon.Stats stats = weapon != null ? weapon.GetStats() : new Weapon.Stats(); // Kiểm tra null cho weapon
            transform.position += transform.right * stats.speed * (weapon != null ? weapon.Owner.Stats.speed : 1f) * Time.fixedDeltaTime; // Kiểm tra null cho weapon và weapon.Owner
            rb.MovePosition(transform.position);
            transform.Rotate(rotationSpeed * Time.fixedDeltaTime);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStats es = other.GetComponent<EnemyStats>();
        BreakableProps p = other.GetComponent<BreakableProps>();

        if (es)
        {
            Vector3 source = damageSource == DamageSource.owner && owner ? owner.transform.position : transform.position;
            es.TakeDamage(GetDamage(), source);

            Weapon.Stats stats = weapon != null ? weapon.GetStats() : new Weapon.Stats(); // Kiểm tra null cho weapon
            piercing--;
            if (stats.hitEffect)
            {
                Destroy(Instantiate(stats.hitEffect, transform.position, Quaternion.identity), 5f);
            }
        }
        else if (p)
        {
            p.TakeDamage(GetDamage());
            piercing--;

            Weapon.Stats stats = weapon != null ? weapon.GetStats() : new Weapon.Stats(); // Kiểm tra null cho weapon
            if (stats.hitEffect)
            {
                Destroy(Instantiate(stats.hitEffect, transform.position, Quaternion.identity), 5f);
            }
        }

        if (piercing <= 0)
        {
            WeaponSpawner.Instance.Despawn(this.transform);
        }
    }
}
