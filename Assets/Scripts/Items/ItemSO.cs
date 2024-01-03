using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/Item")]
public class ItemSO : ScriptableObject
{
    public ItemCode itemCode = ItemCode.NoItem;
    public string itemName;
}
