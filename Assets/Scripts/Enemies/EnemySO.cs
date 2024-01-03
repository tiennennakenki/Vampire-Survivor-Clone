using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public EnemyCode enemyName = EnemyCode.NoEnemy;
    [SerializeField] protected float moveSpeed;
    public float MoveSpeed => moveSpeed;
    [SerializeField] protected float maxHealth;
    public float MaxHealth => maxHealth;
    [SerializeField] protected float damage;
    public float Damage => damage;

    public List<DropRate> dropList;
}
