using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public string waveName;
    public List<EnemyGroup> enemyGroups;
    public int waveQuote;
    public float spawnInterval;
    public int spawnCount;
}
