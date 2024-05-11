using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : Spawner
{
    private static EffectSpawner instance;
    public static EffectSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 EffectSpawner allow to exits");
        instance = this;
    }
}
