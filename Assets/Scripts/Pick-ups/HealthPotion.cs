using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : SaiMonoBehaviour, ICollectable
{
    [SerializeField] protected float healthToRestore = 50;
    public float HealthToRestore => healthToRestore;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(this.healthToRestore);
        ItemsDropSpawner.Instance.Despawn(gameObject.transform);
    }
}
