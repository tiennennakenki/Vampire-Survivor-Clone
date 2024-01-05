using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : SaiMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemsDropSpawner.Instance.Despawn(gameObject.transform);
        }
    }
}
