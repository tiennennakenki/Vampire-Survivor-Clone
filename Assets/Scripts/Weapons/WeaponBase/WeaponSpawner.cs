using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : Spawner
{
    [Header("Weapon Ctrl")]
    public WeaponSO weaponData;
    [SerializeField] protected PlayerMovement playerMovement;

    protected override void Start()
    {
        base.Start();
        this.currentCoolDown = this.weaponData.CoolDownDuration;
        this.playerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected override void Update()
    {
        base.Update();
        this.SpawnWeapon();
    }

    protected override void Attack()
    {
        this.currentCoolDown = this.weaponData.CoolDownDuration;
    }
}
