using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicCtrl : WeaponCtrl
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeaponSO();
    }

    protected virtual void LoadWeaponSO()
    {
        if (this.weaponData != null) return;
        string resPath = "Weapon/GarlicWeapon";
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
        GameObject spawnedGarlic = Instantiate(this.weaponData.prefabs);
        spawnedGarlic.transform.position = transform.position; //Assign position as the same as object which is parented to the player
        spawnedGarlic.transform.parent = transform;
        spawnedGarlic.SetActive(true);
    }
}
