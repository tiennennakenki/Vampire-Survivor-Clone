using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : SaiMonoBehaviour
{
    [Header("Weapon Ctrl")]
    public WeaponSO weaponData;
    [SerializeField] protected float currentCoolDown = 0;
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

    protected virtual void SpawnWeapon()
    {
        this.currentCoolDown -= Time.deltaTime;
        if(this.currentCoolDown <= 0)
        {
            this.Attack();
        }
    }

    protected virtual void Attack()
    {
        this.currentCoolDown = this.weaponData.CoolDownDuration;
    }
}
