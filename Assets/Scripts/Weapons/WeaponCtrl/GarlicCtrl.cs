using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicCtrl : WeaponCtrl
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGarlicPrefab();
    }

    protected virtual void LoadGarlicPrefab()
    {
        if (this.prefabs != null) return;
        this.prefabs = transform.Find("GarlicWeapon").gameObject;
        Debug.LogWarning(transform.name + ": LoadGarlicPrefab", gameObject);
    }
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(prefabs);
        spawnedGarlic.transform.position = transform.position; //Assign position as the same as object which is parented to the player
        spawnedGarlic.transform.parent = transform;
        spawnedGarlic.SetActive(true);
    }
}
