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

    // To represent the bobbing animation of the object.
    //[System.Serializable]
    //public struct BobbingAnimation
    //{
    //    public float frequency;
    //    public Vector2 direction;
    //}
    //public BobbingAnimation bobbingAnimation = new BobbingAnimation
    //{
    //    frequency = 2f,
    //    direction = new Vector2(0, 0.3f)
    //};

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
        //initialOffset = Random.Range(0, bobbingAnimation.frequency);
    }

    protected override void Update()
    {
        base.Update();
        if (!target)
        {
            //transform.position = initialPosition + bobbingAnimation.direction * Mathf.Sin(Time.time * bobbingAnimation.frequency);
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
                    rb2D.AddForce(-distance.normalized * this.speed * 2);
                }
                if (distance.sqrMagnitude >= 1.5)
                {
                    this.isPushed = true;
                    rb2D.AddForce(distance.normalized * this.speed * 10);
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

        //Destroy(gameObject, Mathf.Max(0.01f, this.lifespan));
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundController.Instance.PlayCollectSoundEffect();

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
