using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : Spawner
{
    private static WeaponSpawner instance;
    public static WeaponSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 EffectSpawner allow to exits");
        instance = this;
    }
}
