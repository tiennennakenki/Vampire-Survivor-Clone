using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItem
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpinachPassiveItem();
    }

    protected virtual void LoadSpinachPassiveItem()
    {
        if (this.passiveItemData != null) return;
        string resPath = "Items/Passive Items/Spinach Passive Item/" + transform.name;
        this.passiveItemData = Resources.Load<PassiveItemSO>(resPath);
        Debug.Log(transform.name + ": LoadWingsPassiveItemSO", gameObject);
    }

    protected override void ApplyModifier()
    {
        base.ApplyModifier();
        this.player.CurrentMight *= 1 +  this.passiveItemData.Multipler / 100f;
    }
}
