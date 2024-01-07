using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicSpawner : WeaponSpawner
{
    private static GarlicSpawner instance;
    public static GarlicSpawner Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (GarlicSpawner.instance != null) Debug.LogError("Only 1 GarlicSpawner allow to exit");
        GarlicSpawner.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeaponSO();
    }

    protected virtual void LoadWeaponSO()
    {
        if (this.weaponData != null) return;
        string resPath = "Weapons/GarlicWeapon";
        this.weaponData = Resources.Load<WeaponSO>(resPath);
        Debug.Log(resPath);
        Debug.LogWarning(transform.name + ": LoadWeaponSO", gameObject);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Spawn()
    {
        base.Spawn();
        Transform spawnedGarlic = this.GetObjectFromPool(this.weaponData.prefabs);
        spawnedGarlic.transform.position = transform.position; //Assign position as the same as object which is parented to the player
        spawnedGarlic.transform.parent = this.holder;
        spawnedGarlic.transform.localScale = new Vector3(2, 2, 2);
        spawnedGarlic.gameObject.SetActive(true);
    }
}
