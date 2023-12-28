using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawn
{
    [SerializeField] protected float currentCoolDown = 0;
    [SerializeField] protected float destroyAfterSeconds = 3;
    protected override bool CanDespawn()
    {
        this.currentCoolDown += Time.deltaTime;
        if (this.currentCoolDown >= this.destroyAfterSeconds)
        {
            this.currentCoolDown = 0;
            return true;
        }
        return false;
    }
}
