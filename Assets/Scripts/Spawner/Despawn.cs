using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : SaiMonoBehaviour
{
    protected override void FixedUpdate()
    {
        this.Despawning();
    }

    protected virtual void Despawning()
    {
        if (!CanDespawn()) return;
        this.DespawnObj();
    }

    protected abstract bool CanDespawn();

    public virtual void DespawnObj()
    {
        //Override
    }
}
