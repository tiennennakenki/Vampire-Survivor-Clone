using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : SaiMonoBehaviour, ICollectable
{
    protected bool hasCollected = false;

    protected override void OnDisable()
    {
        base.OnDisable();
        this.hasCollected = false;
    }
    public virtual void Collect()
    {
        this.hasCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemsDropSpawner.Instance.Despawn(gameObject.transform);
        }
    }
}
