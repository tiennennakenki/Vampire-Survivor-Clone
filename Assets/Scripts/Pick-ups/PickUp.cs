using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PickUp : SaiMonoBehaviour
{
    [SerializeField] protected float lifespan = 0.5f;
    [SerializeField] protected PlayerStats target;
    [SerializeField] protected float speed;
    Vector2 initialPosition;
    float initialOffset;
    [SerializeField] bool isPushed = false;

    [Header("Bonuses")]
    public int experience;
    public int health;
    public float coin;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        initialPosition = transform.position;
    }

    protected override void Update()
    {
        this.MoveItemToPlayer();
    }

    protected virtual void MoveItemToPlayer()
    {
        if (!target)
        {
            transform.position = initialPosition;
        }
        else
        {
            Vector2 distance = this.target.transform.position - transform.position;
            if (distance.sqrMagnitude > 0)
            {
                Rigidbody2D rb2D = this.GetComponent<Rigidbody2D>();
                if (!this.isPushed)
                {
                    rb2D.AddForce(-distance.normalized * 200);
                }
                if (distance.sqrMagnitude >= 1.5f)
                {
                    this.isPushed = true;
                    rb2D.AddForce(distance.normalized * 200);
                }
            }
            else
            {
                this.target = null;
                ItemsDropSpawner.Instance.Despawn(gameObject.transform);
            }
        }
    }


    public virtual bool Collect(PlayerStats target, float speed, float lifespan = 0f)
    {
        if (!target) return false;

        this.target = target;
        this.speed = speed;

        if (lifespan > 0)
        {
            this.lifespan = lifespan;
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(this.target == null) return;
            SoundManager.Instance.PlayCollectSoundEffect();

            if (experience != 0) target.IncreaseExperience(experience);
            if (health != 0) target.RestoreHealth(health);
            if(coin != 0)
            {
                GameManager.Instance.IncreaseCoin(coin);
            }

            this.target = null;
            this.isPushed = false;
            ItemsDropSpawner.Instance.Despawn(gameObject.transform);
        }
    }
}
