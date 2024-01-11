using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStats : SaiMonoBehaviour
{
    [Header("Player Stats")]
    public CharacterSO characterData;

    //Current stats
    public GameObject currentStartingWeapon;
    public float currentHealth;
    public float currentRecovery;
    public float currentMight;
    public float currentMoveSpeed;
    public float currentProjectilesSpeed;
    public float currentMagnet;

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
    [SerializeField] protected InventoryManager inventory;
    [SerializeField] protected int weaponIndex;
    [SerializeField] protected int passiveItemIndex;

    public GameObject passiveItemTest1, passiveItemTest2;
    public GameObject weaponTest2;

    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value == currentHealth) return;
            this.currentHealth = value;
            if (GameManager.Instance == null) return;
            GameManager.Instance.currentHealthDisplay.text = "Health: " + this.currentHealth;
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (value == currentRecovery) return;
            this.currentRecovery = value;
            if (GameManager.Instance == null) return;
            GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + this.currentRecovery;
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (value == currentMight) return;
            this.currentMight = value;
            if (GameManager.Instance == null) return;
            GameManager.Instance.currentMightDisplay.text = "Might: " + this.CurrentMight;
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (value == currentMoveSpeed) return;
            this.currentMoveSpeed = value;
            if (GameManager.Instance == null) return;
            GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: " + this.currentMoveSpeed;
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectilesSpeed; }
        set
        {
            if (value == currentProjectilesSpeed) return;
            this.currentProjectilesSpeed = value;
            if (GameManager.Instance == null) return;
            GameManager.Instance.currentProjectilesSpeedDisplay.text = "Projectiles: " + this.currentProjectilesSpeed;
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (value == currentMagnet) return; 
            this.currentMagnet = value;
            if (GameManager.Instance == null) return;
            GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + this.currentMagnet;
        }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        //Assign the variables
        this.characterData = CharacterCollector.Instance.GetData();
        CharacterCollector.Instance.DestroySingleton();

        this.currentStartingWeapon = this.characterData.StartingWeapon;
        this.CurrentHealth = this.characterData.MaxHealth;
        this.CurrentRecovery = this.characterData.Recovery;
        this.CurrentMight = this.characterData.Might;
        this.CurrentMoveSpeed = this.characterData.MoveSpeed;
        this.CurrentProjectileSpeed = this.characterData.ProjectileSpeed;
        this.CurrentMagnet = this.characterData.Magnet;

        //Inventory
        this.inventory = FindObjectOfType<InventoryManager>();
    }

    protected override void Start()
    {
        base.Start();
        //Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;
        this.SpawnWeapon(this.currentStartingWeapon);
        //this.SpawnWeapon(this.weaponTest2);
        this.SpawnPassiveItem(this.passiveItemTest1);
        this.SpawnPassiveItem(this.passiveItemTest2);

        //Set the current stats display
        GameManager.Instance.currentHealthDisplay.text = "Health: " + this.currentHealth;
        GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + this.currentRecovery;
        GameManager.Instance.currentMightDisplay.text = "Might: " + this.CurrentMight;
        GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: " + this.currentMoveSpeed;
        GameManager.Instance.currentProjectilesSpeedDisplay.text = "Projectiles: " + this.currentProjectilesSpeed;
        GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + this.currentMagnet;

        GameManager.Instance.AssignCharacterDataUI(this.characterData);
    }

    protected override void Update()
    {
        base.Update();
        this.ResetInvinciblility();
    }

    public virtual void IncreaseExperience(int amount)
    {
        this.experience += amount;

        this.LevelUpChecker();
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
        }
    }

    public virtual void TakeDamage(float amount)
    {
        //If the player is not currently invincible, reduce health and start invincibility 
        if (!isInvincible)
        {
            this.CurrentHealth -= amount;
            //UIHPBar.Instance.UpdateHpBar();

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

    protected virtual void Kill()
    {
        if (GameManager.Instance.isGameOver) return;
        GameManager.Instance.AssignLevelReachedUI(this.level);
        GameManager.Instance.AssignWeaponAndPassiveItemUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
        GameManager.Instance.GameOver();
    }

    public virtual void RestoreHealth(float health)
    {
        if (this.CurrentHealth >= this.characterData.MaxHealth) return;
        this.CurrentHealth += health;
        if(this.CurrentHealth > this.characterData.MaxHealth)
        {
            this.CurrentHealth = this.characterData.MaxHealth;
        }
    }

    protected virtual void Recover()
    {
        if (this.CurrentHealth >= this.characterData.MaxHealth) return;
        this.CurrentHealth += this.CurrentRecovery * Time.deltaTime;
        if(this.CurrentHealth > this.characterData.MaxHealth)
        {
            this.CurrentHealth = this.characterData.MaxHealth;
        }
    }

    protected virtual void SpawnWeapon(GameObject weapon)
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

                inventory.AddWeapon(weaponIndex, skill.GetComponent<WeaponSpawner>());
                weaponIndex++;
            }
        }
    }

    protected virtual void SpawnPassiveItem(GameObject passiveItem)
    {
        if (this.passiveItemIndex >= this.inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Passive Item slots already full");
            return;
        }

        foreach (GameObject item in WeaponCtrl.Instance.passiveItems)
        {
            if (passiveItem.ToString() == item.ToString())
            {
                item.gameObject.SetActive(true);

                inventory.AddPassiveItem(passiveItemIndex, item.GetComponent<PassiveItem>());
                passiveItemIndex++;
            }
        }
    }
}
