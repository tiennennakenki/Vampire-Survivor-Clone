using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDespawn : DespawnByTime
{
    public override void DespawnObj()
    {
        KnifeSpawner.Instance.Despawn(transform.parent);
        Debug.Log(transform.parent);
    }
}
