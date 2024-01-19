using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : SaiMonoBehaviour, ICollectable
{
    public bool hasBeenCollected = false;

    protected override void OnDisable()
    {
        base.OnDisable();
        this.hasBeenCollected = false;
    }
    public virtual void Collect()
    {
        this.hasBeenCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemsDropSpawner.Instance.Despawn(gameObject.transform);
        }
    }
}
