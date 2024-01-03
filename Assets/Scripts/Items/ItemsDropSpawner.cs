using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDropSpawner : Spawner
{
    private static ItemsDropSpawner instance;
    public static ItemsDropSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (ItemsDropSpawner.instance != null) Debug.Log("Only 1 ItemsDropSpawner allow to exits");
        ItemsDropSpawner.instance = this;
    }

    public virtual void Drop(List<DropRate> dropList, Vector3 pos, Quaternion rot)
    {
        ItemCode itemCode = dropList[0].itemSO.itemCode;
        Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
        itemDrop.gameObject.SetActive(true);
    }

}
