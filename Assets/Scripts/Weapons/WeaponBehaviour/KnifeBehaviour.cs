using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKnifeSO();
    }
    protected virtual void LoadKnifeSO()
    {
        if (this.weaponData != null) return;
        string resPath = "Weapon/" + transform.name;
        this.weaponData = Resources.Load<WeaponSO>(resPath);
        Debug.Log(resPath);
        Debug.LogWarning(transform.name + ": LoadKnifeSO", gameObject);
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        this.SpawnKnife();
    }

    protected virtual void SpawnKnife()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    
}
