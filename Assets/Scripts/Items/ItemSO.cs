using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "2D Top-down Rogue-like/Item SO")]
public class ItemSO : ScriptableObject
{
    public ItemCode itemCode = ItemCode.NoItem;
    public string itemName;
}