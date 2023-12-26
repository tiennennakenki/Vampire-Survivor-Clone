using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : SaiMonoBehaviour
{
    [Header("Weapon Ctrl")]
    [SerializeField] protected GameObject prefabs;
    [SerializeField] protected int damage;
    [SerializeField] public float speed = 10;
    [SerializeField] protected float coolDownDuration = 3;
    [SerializeField] protected float currentCoolDown = 0;
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected float pierce;

    protected override void Start()
    {
        base.Start();
        this.currentCoolDown = this.coolDownDuration;
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
        this.currentCoolDown = this.coolDownDuration;
    }
}
