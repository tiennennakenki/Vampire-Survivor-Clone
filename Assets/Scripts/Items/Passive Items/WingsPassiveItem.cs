using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWingsPassiveItemSO();
    }

    protected virtual void LoadWingsPassiveItemSO()
    {
        if (this.passiveItemData != null) return;
        string resPath = "Items/Passive Items/Wings Passive Item/" + transform.name;
        this.passiveItemData = Resources.Load<PassiveItemSO>(resPath);
        Debug.Log(transform.name + ": LoadWingsPassiveItemSO", gameObject);
    }

    protected override void ApplyModifier()
    {
        base.ApplyModifier();
        this.player.currentMoveSpeed *= 1 + this.passiveItemData.Multipler/100f;
    }
}
