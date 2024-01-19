using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperienceGem : PickUp
{
    [SerializeField] protected int experienceGranted = 10;
    public int ExperienceGranted => experienceGranted;
    public override void Collect()
    {
        if(this.hasBeenCollected) return;
        base.Collect();
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.IncreaseExperience(experienceGranted);
        //ItemsDropSpawner.Instance.Despawn(gameObject.transform);
    }
}
