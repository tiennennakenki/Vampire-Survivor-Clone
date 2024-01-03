using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : SaiMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }
}
