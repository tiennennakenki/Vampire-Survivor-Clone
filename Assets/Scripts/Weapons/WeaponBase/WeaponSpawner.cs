using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : Spawner
{
    [Header("Weapon Ctrl")]
    public WeaponSO weaponData;
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected Transform player;

    protected override void Start()
    {
        base.Start();
        this.currentCoolDown = this.weaponData.CoolDownDuration;
        this.playerMovement = FindObjectOfType<PlayerMovement>();
        this.player = playerMovement.transform.parent;
    }

    protected override void Update()
    {
        base.Update();
        this.SpawnWeapon();
        this.FollowPlayer();
    }

    protected override void Attack()
    {
        this.currentCoolDown = this.weaponData.CoolDownDuration;
    }

    protected virtual void FollowPlayer()
    {
        this.transform.position = new Vector2 (this.player.position.x, this.player.position.y);
    }
}
