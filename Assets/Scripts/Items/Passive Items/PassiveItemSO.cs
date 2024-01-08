using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemSO", menuName = "SO/PassiveItem")]
public class PassiveItemSO : ScriptableObject
{
    [SerializeField] protected float multipler;
    public float Multipler => multipler;
    [SerializeField] protected int level;
    public int Level => level;
    [SerializeField] protected GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab => nextLevelPrefab;
    [SerializeField] protected Sprite icon;
    public Sprite Icon => icon;
}
