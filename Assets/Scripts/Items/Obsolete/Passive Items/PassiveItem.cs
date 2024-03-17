using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : SaiMonoBehaviour
{
    [SerializeField] protected PlayerStats player;
    public PassiveItemSO passiveItemData;


    protected override void Start()
    {
        base.Start();
        this.player = FindObjectOfType<PlayerStats>();
        this.ApplyModifier();
    }

    protected virtual void ApplyModifier()
    {
        //For override
    }
}
