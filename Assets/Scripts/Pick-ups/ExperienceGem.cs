using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : SaiMonoBehaviour, ICollectable
{
    [SerializeField] protected int experienceGranted = 10;
    public int ExperienceGranted => experienceGranted;
    public void Collect()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.IncreaseExperience(experienceGranted);
        ItemsDropSpawner.Instance.Despawn(gameObject.transform);
    }
}
