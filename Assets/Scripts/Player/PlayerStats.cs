using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerStats : SaiMonoBehaviour
{
    [Header("Player Stats")]
    public CharacterData characterData;
    public CharacterData.Stats baseStats;
    [SerializeField] CharacterData.Stats actualStats;

    ////Current stats
    //public GameObject currentStartingWeapon;
    //public float currentHealth;
    //public float currentRecovery;
    //public float currentMight;
    //public float currentMoveSpeed;
    //public float currentProjectilesSpeed;
    //public float currentMagnet;
    float health;

    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    public List<LevelRange> levelRanges;

    //I-frame
    protected float invincibilityDuration = .5f;
    protected float invincibilityTimer;
    protected bool isInvincible;

    //Inventory
    [SerializeField] PlayerInventory inventory;
    [SerializeField] protected int weaponIndex;
    [SerializeField] protected int passiveItemIndex;

    //public GameObject passiveItemTest1, passiveItemTest2;
    //public GameObject weaponTest2;

    //UI
    [Header("UI")]
    [SerializeField] protected Image expBar;
    [SerializeField] protected TextMeshProUGUI levelText;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return health; }
        set
        {
            if (value == health) return;
            this.health = value;
            if (GameManager.Instance == null) return;
            GameManager.Instance.currentHealthDisplay.text = string.Format(
                        "Health: {0} / {1}",
                        health, actualStats.maxHealth
                    );
        }
    }

    public float MaxHealth
    {
        get { return actualStats.maxHealth; }

        // If we try and set the max health, the UI interface
        // on the pause screen will also be updated.
        set
        {
            //Check if the value has changed
            if (actualStats.maxHealth != value)
            {
                actualStats.maxHealth = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentHealthDisplay.text = string.Format(
                        "Health: {0} / {1}",
                        health, actualStats.maxHealth
                    );
                }
                //Update the real time value of the stat
                //Add any additional logic here that needs to be executed when the value changes
            }
        }
    }

    public float CurrentRecovery
    {
        get { return Recovery; }
        set { Recovery = value; }
    }

    public float Recovery
    {
        get { return actualStats.recovery; }
        set
        {
            //Check if the value has changed
            if (actualStats.recovery != value)
            {
                actualStats.recovery = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + actualStats.recovery;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return Might; }
        set { Might = value; }
    }
    public float Might
    {
        get { return actualStats.might; }
        set
        {
            //Check if the value has changed
            if (actualStats.might != value)
            {
                actualStats.might = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMightDisplay.text = "Might: " + actualStats.might;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return MoveSpeed; }
        set { MoveSpeed = value; }
    }
    public float MoveSpeed
    {
        get { return actualStats.moveSpeed; }
        set
        {
            //Check if the value has changed
            if (actualStats.moveSpeed != value)
            {
                actualStats.moveSpeed = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: " + actualStats.moveSpeed;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return Speed; }
        set { Speed = value; }
    }
    public float Speed
    {
        get { return actualStats.speed; }
        set
        {
            //Check if the value has changed
            if (actualStats.speed != value)
            {
                actualStats.speed = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + actualStats.speed;
                }
            }
        }
    }


    public float CurrentMagnet
    {
        get { return Magnet; }
        set { Magnet = value; }
    }
    public float Magnet
    {
        get { return actualStats.magnet; }
        set
        {
            //Check if the value has changed
            if (actualStats.magnet != value)
            {
                actualStats.magnet = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + actualStats.magnet;
                }
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

        //Assign the variables
        baseStats = actualStats = characterData.stats;
        health = actualStats.maxHealth;

        //Inventory
        this.inventory = GetComponent<PlayerInventory>();
    }

    protected override void Start()
    {
        base.Start();
        //Spawn the starting weapon
        inventory.Add(characterData.StartingWeapon);

        //Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;
        //this.SpawnWeapon(this.currentStartingWeapon);
        //this.SpawnWeapon(this.weaponTest2);
        //this.SpawnPassiveItem(this.passiveItemTest1);
        //this.SpawnPassiveItem(this.passiveItemTest2);

        //Set the current stats display
        GameManager.Instance.currentHealthDisplay.text = "Health: " + this.CurrentHealth;
        GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + this.CurrentRecovery;
        GameManager.Instance.currentMightDisplay.text = "Might: " + this.CurrentMight;
        GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: " + this.CurrentMoveSpeed;
        GameManager.Instance.currentProjectileSpeedDisplay.text = "Projectiles: " + this.CurrentProjectileSpeed;
        GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + this.CurrentMagnet;

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
        actualStats = baseStats;
        foreach (PlayerInventory.Slot s in inventory.passiveSlots)
        {
            Passive p = s.item as Passive;
            if (p)
            {
                actualStats += p.GetBoosts();
            }
        }
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
            this.CurrentHealth -= amount;

            this.invincibilityTimer = this.invincibilityDuration;
            this.isInvincible= true;

            if (this.CurrentHealth <= 0)
            {
                this.Kill();
            }
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
        if (this.CurrentHealth >= actualStats.maxHealth) return;
        this.CurrentHealth += this.CurrentRecovery * Time.deltaTime;
        if(this.CurrentHealth > actualStats.maxHealth)
        {
            this.CurrentHealth = actualStats.maxHealth;
        }
    }

    [System.Obsolete("Old function that is kept to maintain compatibility with the InventoryManager. Will be removed soon.")]
    public virtual void SpawnWeapon(GameObject weapon)
    {
        if (this.weaponIndex >= this.inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Weapon slots already full");
            return;
        }

        foreach (GameObject skill in WeaponCtrl.Instance.listSkills)
        {
            if(weapon.ToString() == skill.ToString())
            {
                //Debug.Log("Checked");
                skill.gameObject.SetActive(true);

                //inventory.AddWeapon(weaponIndex, skill.GetComponent<WeaponSpawner>());
                weaponIndex++;
            }
        }
    }

    [System.Obsolete("No need to spawn passive items directly now.")]
    public virtual void SpawnPassiveItem(GameObject passiveItem)
    {
        if (this.passiveItemIndex >= this.inventory.passiveSlots.Count - 1)
        {
            Debug.LogError("Passive Item slots already full");
            return;
        }

        foreach (GameObject item in WeaponCtrl.Instance.passiveItems)
        {
            if (passiveItem.ToString() == item.ToString())
            {
                item.gameObject.SetActive(true);

                //inventory.AddPassiveItem(passiveItemIndex, item.GetComponent<PassiveItem>());
                passiveItemIndex++;
            }
        }
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
