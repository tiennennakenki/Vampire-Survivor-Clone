using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] public Transform prefabs;
    [SerializeField] protected float damage;
    public float Damage => damage;
    [SerializeField] public float speed;
    public float Speed => speed;
    [SerializeField] protected float coolDownDuration;
    public float CoolDownDuration => coolDownDuration;
    [SerializeField] protected float pierce;
    public float Pierce => pierce;
}
