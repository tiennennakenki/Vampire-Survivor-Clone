using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCtrl : WeaponCtrl
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKnifePrefab();
    }

    protected virtual void LoadKnifePrefab()
    {
        if (this.prefabs != null) return;
        this.prefabs = transform.Find("KnifeWeapon").gameObject;
        Debug.LogWarning(transform.name + ": LoadKnifePrefab", gameObject);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(prefabs);
        spawnedKnife.transform.position = transform.position; //Assign the position to be the same as this object which is parented to the player
        spawnedKnife.SetActive(true);
        spawnedKnife.transform.parent = transform;
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(playerMovement.lastMovedVector);
    }
}
