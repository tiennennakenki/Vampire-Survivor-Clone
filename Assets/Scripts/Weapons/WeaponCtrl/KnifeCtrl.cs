using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCtrl : WeaponCtrl
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeaponSO();
    }

    protected virtual void LoadWeaponSO()
    {
        if (this.weaponData != null) return;
        string resPath = "Weapon/KnifeWeapon";
        this.weaponData = Resources.Load<WeaponSO>(resPath);
        Debug.Log(resPath);
        Debug.LogWarning(transform.name + ": LoadWeaponSO", gameObject);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(this.weaponData.prefabs);
        spawnedKnife.transform.position = transform.position; //Assign the position to be the same as this object which is parented to the player
        spawnedKnife.SetActive(true);
        spawnedKnife.transform.parent = transform;
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(playerMovement.lastMovedVector);
    }
}
