using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicDespawn : DespawnByTime
{
    public override void DespawnObj()
    {
        GarlicSpawner.Instance.Despawn(transform.parent);
    }
}
