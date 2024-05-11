using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFloatingTextSpawner : Spawner
{
    private static DamageFloatingTextSpawner instance;
    public static DamageFloatingTextSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 DamageFloatingTextSpawner allow to exits");
        instance = this;
    }
}
