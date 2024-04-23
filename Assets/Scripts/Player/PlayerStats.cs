using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerStats : SaiMonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] protected PlayerCtrl playerCtrl;
    public CharacterData characterData;
    public CharacterData.Stats baseStats;
    [SerializeField] CharacterData.Stats actualStats;

    public CharacterData.Stats Stats
    {
        get { return actualStats; }
        set
        {
            actualStats = value;
        }
    }

    public float health;

    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    public List<LevelRange> levelRanges;

    [Header("Visuals")]
    public ParticleSystem blockedEffect; // If armor completely blocks damage.
    public ParticleSystem damageEffect;

    //I-frame
    protected float invincibilityDuration = .5f;
    protected float invincibilityTimer;
    protected bool isInvincible;

    //Inventory
    [SerializeField] PlayerInventory inventory;
    [SerializeField] PlayerCollector collector;
    [SerializeField] protected int weaponIndex;
    [SerializeField] protected int passiveItemIndex;

    //UI
    [Header("UI")]
    [SerializeField] protected Image expBar;
    [SerializeField] protected TextMeshProUGUI levelText;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return health; }

        // If we try and set the current health, the UI interface
        // on the pause screen will also be updated.
        set
        {
            if(this.health != value)
            {
                this.health = value;
            }
        }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        this.characterData = CharacterCollector.Instance.GetData();

        if (CharacterCollector.Instance)
            CharacterCollector.Instance.DestroySingleton();

        //Inventory
        this.collector = playerCtrl.Collector;

        //Assign the variables
        baseStats = actualStats = characterData.stats;
        health = actualStats.maxHealth;
        collector.SetRadius(actualStats.magnet);
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadInventory();
        this.LoadCollector();
        //this.LoadExperienceBar();
    }

    protected virtual void LoadExperienceBar()
    {
        if (this.expBar != null) return;
        Canvas canvas = GameObject.Find("UI_Canvas").GetComponent<Canvas>();
        if (canvas != null)
        {
            Transform experienceBarHolder = canvas.transform.Find("Experience Bar Holder");
            if (experienceBarHolder != null)
            {
                this.expBar = experienceBarHolder.Find("Experience Bar")?.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("Experience Bar Holder not found!");
            }
        }
        else
        {
            Debug.LogError("Canvas not found!");
        }

        Debug.LogWarning(transform.name + ": LoadExperienceBar", gameObject);
    }


    protected virtual void LoadPlayerCtrl()
    {
        if(this.playerCtrl != null) return;
        this.playerCtrl = gameObject.GetComponentInParent<PlayerCtrl>();

        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = this.GetComponent<PlayerInventory>();

        Debug.LogWarning(transform.name + ": LoadInventory", gameObject);
    }

    protected virtual void LoadCollector()
    {
        if (this.collector != null) return;
        this.collector = this.playerCtrl.Collector;

        Debug.LogWarning(transform.name + ": LoadCollector", gameObject);
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        //Spawn the starting weapon
        inventory.Add(characterData.StartingWeapon);

        //Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.Instance.AssignCharacterDataUI(this.characterData);

        this.UpdateExpBar();
        this.UpdateLevelText();
    }

    protected override void Update()
    {
        base.Update();
        this.ResetInvinciblility();
    }

    public void RecalculateStats()
    {
        //baseStats = actualStats;
        actualStats = baseStats;
        foreach (PlayerInventory.Slot s in inventory.passiveSlots)
        {
            Passive p = s.item as Passive;
            if (p)
            {
                actualStats += p.GetBoosts();
            }
        }

        // Update the PlayerCollector's radius.
        collector.SetRadius(actualStats.magnet);
    }

    public virtual void IncreaseExperience(int amount)
    {
        this.experience += amount;

        this.LevelUpChecker();
        this.UpdateExpBar();
    }

    protected virtual void LevelUpChecker()
    {
        if(this.experience >= this.experienceCap)
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
            this.UpdateLevelText();

            GameManager.Instance.StartLevelUI();
        }
    }

    public virtual void TakeDamage(float amount)
    {
        //If the player is not currently invincible, reduce health and start invincibility 
        if (!isInvincible)
        {
            // Take armor into account before dealing the damage.
            amount -= actualStats.armor;

            if (amount > 0)
            {
                // Deal the damage.
                CurrentHealth -= amount;

                invincibilityTimer = invincibilityDuration;
                isInvincible = true;
                // If there is a damage effect assigned, play it.
                if (damageEffect) Destroy(Instantiate(damageEffect, transform.position, Quaternion.identity), 5f);

                //Play character hurt sound effect
                SoundController.Instance.PlayCharacterHurtSoundEffect();

                if (CurrentHealth <= 0)
                {
                    Kill();
                }
            }
            else
            {
                // If there is a blocked effect assigned, play it.
                if (blockedEffect) Destroy(Instantiate(blockedEffect, transform.position, Quaternion.identity), 5f);
            }

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
        }

    }

    protected virtual void ResetInvinciblility()
    {
        if(this.invincibilityTimer > 0)
        {
            this.invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            this.isInvincible = false;
        }

        this.Recover();
    }

    public virtual void Kill()
    {
        if (GameManager.Instance.isGameOver) return;
        
        GameManager.Instance.AssignLevelReachedUI(this.level);
        GameManager.Instance.AssignWeaponAndPassiveItemUI(inventory.weaponSlots, inventory.passiveSlots);
        GameManager.Instance.GameOver();
    }

    public virtual void RestoreHealth(float health)
    {
        if (this.CurrentHealth >= actualStats.maxHealth) return;
        this.CurrentHealth += health;
        if(this.CurrentHealth > actualStats.maxHealth)
        {
            this.CurrentHealth = actualStats.maxHealth;
        }
    }
    protected virtual void Recover()
    {
        if (CurrentHealth < actualStats.maxHealth)
        {
            CurrentHealth += Stats.recovery * Time.deltaTime;

            // Make sure the player's health doesn't exceed their maximum health
            if (CurrentHealth > actualStats.maxHealth)
            {
                CurrentHealth = actualStats.maxHealth;
            }
        }

        this.UpdateExpBar();
    }

    protected virtual void UpdateExpBar()
    {
        //Update exp bar fill amount
        this.expBar.fillAmount = (float)this.experience / this.experienceCap;
    }

    protected virtual void UpdateLevelText()
    {
        this.levelText.text = "LV: " + level.ToString();
    }
}
