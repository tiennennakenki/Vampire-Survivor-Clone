using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDropSpawner : Spawner
{
    private static ItemsDropSpawner instance;
    public static ItemsDropSpawner Instance => instance;

    [SerializeField] protected float gameDropRate = 1;

    protected override void Awake()
    {
        base.Awake();
        if (ItemsDropSpawner.instance != null) Debug.Log("Only 1 ItemsDropSpawner allow to exits");
        ItemsDropSpawner.instance = this;
    }

    public virtual List<DropRate> Drop(List<DropRate> dropList, Vector3 pos, Quaternion rot)
    {
        List<DropRate> dropItems = new List<DropRate>();

        if (dropList.Count < 1) return dropItems;

        dropItems = this.DropItems(dropList);
        foreach (DropRate itemDropRate in dropItems)
        {
            ItemCode itemCode = itemDropRate.itemSO.itemCode;
            Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
            if (itemDrop == null) continue;
            itemDrop.gameObject.SetActive(true);
        }

        return dropItems;
    }

    protected virtual List<DropRate> DropItems(List<DropRate> items)
    {
        List<DropRate> droppedItems = new List<DropRate>();

        float rate, itemRate;
        int itemDropMore;
        foreach (DropRate item in items)
        {
            rate = Random.Range(0, 1f);
            itemRate = item.dropRate / 100000f * this.GameDropRate();
            itemDropMore = Mathf.FloorToInt(itemRate);
            if (itemDropMore > 0)
            {
                itemRate -= itemDropMore;
                for (int i = 0; i < itemDropMore; i++)
                {
                    droppedItems.Add(item);
                }
            }

            //Debug.Log("=====================");
            //Debug.Log("item: " + item.itemSO.itemName);
            //Debug.Log("rate: " + itemRate + "/" + rate);
            //Debug.Log("itemRate: " + itemRate);
            //Debug.Log("itemDropMore: " + itemDropMore);

            if (rate <= itemRate)
            {
                //Debug.Log("DROPED");
                droppedItems.Add(item);
            }
        }

        return droppedItems;
    }

    protected virtual float GameDropRate()
    {
        float dropRateFromItems = 0f;

        return this.gameDropRate + dropRateFromItems;
    }
}