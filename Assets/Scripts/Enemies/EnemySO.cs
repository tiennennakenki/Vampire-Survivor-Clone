using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] protected float moveSpeed;
    public float MoveSpeed => moveSpeed;
    [SerializeField] protected float maxHealth;
    public float MaxHealth => maxHealth;
    [SerializeField] protected float damage;
    public float Damage => damage;
}
