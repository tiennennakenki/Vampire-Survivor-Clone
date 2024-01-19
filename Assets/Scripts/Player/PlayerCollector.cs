using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : SaiMonoBehaviour
{
    [SerializeField] protected PlayerStats player;
    [SerializeField] protected CircleCollider2D playerCollector;
    [SerializeField] protected float pullSpeed = 300f;

    protected override void Start()
    {
        base.Start();
        this.player = FindObjectOfType<PlayerStats>();
        this.playerCollector = transform.GetComponent<CircleCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        this.LoadPlayerCollector();
    }

    protected virtual void LoadPlayerCollector()
    {
        this.playerCollector.radius = this.player.CurrentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the other game object has a ICollectable interface
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            //Pulling animation
            //Gets the Rigidbody2D component in the item
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            //Vector2 pointing from the item to the player
            Vector2 forceDirection = (transform.parent.position - collision.transform.position).normalized;
            //Applies force to the item in the forceDirection with pullSpeed
            rb.AddForce(forceDirection * this.pullSpeed);

            collectable.Collect();
        }
    }
}
