using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : WeaponSpawner
{
    [Header("Knife KnifeSpawnerSpawner")]
    private static KnifeSpawner instance;
    public static KnifeSpawner Instance { get => instance; }


    protected override void Awake()
    {
        base.Awake();
        if (KnifeSpawner.instance != null) Debug.Log("Only 1 KnifeSpawner allow to exit");
        KnifeSpawner.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeaponSO();
    }

    protected virtual void LoadWeaponSO()
    {
        if (this.weaponData != null) return;
        string resPath = "Weapons/Knife Weapon/" + transform.name;
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

        Transform spawnedKnife = this.GetObjectFromPool(this.weaponData.prefabs);
        //Transform spawnedKnife = Instantiate(this.weaponData.prefabs);
        spawnedKnife.gameObject.SetActive(true);
        spawnedKnife.transform.position = transform.position; //Assign the position to be the same as this object which is parented to the player
        spawnedKnife.transform.parent = this.holder;
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(playerMovement.lastMovedVector);
    }
}
