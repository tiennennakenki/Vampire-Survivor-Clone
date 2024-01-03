using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStats : SaiMonoBehaviour
{
    [Header("Player Stats")]
    public CharacterSO characterData;

    //Current stats
    [SerializeField] protected float currentMaxHealth;
    [SerializeField] protected float currentRecovery;
    [SerializeField] protected float currentMight;
    [SerializeField] protected float currentMoveSpeed;
    [SerializeField] protected float currentProjectileSpeed;

    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    public List<LevelRange> levelRanges;

    protected override void Awake()
    {
        base.Awake();
        //Assign the variables
        this.currentMaxHealth = this.characterData.MaxHealth;
        this.currentRecovery = this.characterData.Recovery;
        this.currentMight = this.characterData.Might;
        this.currentMoveSpeed = this.characterData.MoveSpeed;
        this.currentProjectileSpeed = this.characterData.ProjectileSpeed;
    }

    protected override void Start()
    {
        base.Start();
        //Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    public virtual void IncreaseExperience(int amount)
    {
        this.experience += amount;

        this.LevelUpChecker();
    }

    protected virtual void LevelUpChecker()
    {
        if(this.experience > this.experienceCap)
        {
            level++;
            this.experience -= this.experienceCap;

            int experienceCapIncrease = 0;
            foreach(LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            this.experienceCap += experienceCapIncrease;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterSO();
    }

    protected virtual void LoadCharacterSO()
    {
        if (this.characterData != null) return;
        string resPath = "Characters/KnightCharacter";
        this.characterData = Resources.Load<CharacterSO>(resPath);
        Debug.Log(resPath);
        Debug.LogWarning(transform.name + ": LoadCharacterSO", gameObject);
    }
}
