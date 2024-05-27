using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyStats : SaiMonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemySO enemyData;

    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1); //what the color of the damage flash should be
    public float damageFlashDuration = 0.2f; //How long the flash should last
    public float deathFadeTime = 1f; //How much time it takes for enemy to fade
    protected Color originalColor;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected EnemyMovement enemyMovement;

    public bool isDead;

    protected override void Awake()
    {
        base.Awake();
        this.currentMoveSpeed = this.enemyData.MoveSpeed;
        this.currentHealth = this.enemyData.MaxHealth;
        this.currentDamage = this.enemyData.Damage;
    }

    protected override void Start()
    {
        this.originalColor = spriteRenderer.color;
        this.isDead = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.isDead = false;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySO();
        this.LoadSpriteRenderer();
        this.LoadEnemyMovement();
    }

    protected virtual void LoadEnemySO()
    {
        if (this.enemyData != null) return;
        string resPath = "Enemies/" + transform.name;
        this.enemyData = Resources.Load<EnemySO>(resPath);
        Debug.Log(resPath);
        Debug.LogWarning(transform.name + ": LoadBatEnemySO", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;

        this.spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    protected virtual void LoadEnemyMovement()
    {
        if (this.enemyMovement != null) return;

        this.enemyMovement = GetComponent<EnemyMovement>();
        Debug.LogWarning(transform.name + ": LoadEnemyMovement", gameObject);
    }
    #endregion

    public virtual void TakeDamage(float damage, Vector2 sourcePosition, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        if(this.isDead) return;
        this.currentHealth -= damage;
        StartCoroutine(DamageFlash());
        SoundManager.Instance.PlayEnemyHurtSoundEffect();

        if(damage > 0)
        {
            GameManager.Instance.GenerateFloatingText(Mathf.FloorToInt(damage).ToString(), transform);
        }

        if(knockbackForce > 0)
        {
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            enemyMovement.Knockback(dir.normalized * knockbackForce, knockbackDuration);
        }

        if(this.currentHealth <= 0 )
        {
            this.isDead = true;
            this.OnDead();
        }
    }

    protected virtual void OnDead()
    {
        StartCoroutine(KillFale());
    }

    IEnumerator KillFale()
    {
        
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, originalAlpha = spriteRenderer.color.a;

        while(t < deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (1 - t / deathFadeTime) * originalAlpha);
        }

        this.OnDeadDrop();
        EnemiesSpawner.Instance.OnEnemyKilled();
        EnemiesSpawner.Instance.Despawn(transform);
        GameManager.Instance.IncreaseEnemiesDead();
    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = this.transform.position;
        Quaternion dropRot = this.transform.rotation;
        ItemsDropSpawner.Instance.Drop(this.enemyData.dropList, dropPos, dropRot);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
        //    player.TakeDamage(currentDamage);
        //}

        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Debug.Log("collision with enemy");
        //}
        if(isDead) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
        player.TakeDamage(currentDamage);
    }

    public virtual void ResetCurrentHealth()
    {
        this.currentHealth = this.enemyData.MaxHealth;
    }

    //This is a Coroutine function that makes the enemy flash when taking damage
    IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(this.damageFlashDuration);
        spriteRenderer.color = originalColor;
    }
}
