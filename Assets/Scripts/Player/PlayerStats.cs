using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStats : SaiMonoBehaviour
{
    [Header("Player Stats")]
    public CharacterSO characterData;

    //Current stats
    [SerializeField] public GameObject currentStartingWeapon;
    [SerializeField] public float currentHealth;
    [SerializeField] public float currentRecovery;
    [SerializeField] public float currentMight;
    [SerializeField] public float currentMoveSpeed;
    [SerializeField] public float currentProjectileSpeed;
    [SerializeField] public float currentMagnet;

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

    protected override void Awake()
    {
        base.Awake();
        //Assign the variables
        this.characterData = CharacterCollector.Instance.GetData();
        CharacterCollector.Instance.DestroySingleton();

        this.currentStartingWeapon = this.characterData.StartingWeapon;
        this.currentHealth = this.characterData.MaxHealth;
        this.currentRecovery = this.characterData.Recovery;
        this.currentMight = this.characterData.Might;
        this.currentMoveSpeed = this.characterData.MoveSpeed;
        this.currentProjectileSpeed = this.characterData.ProjectileSpeed;
        this.currentMagnet = this.characterData.Magnet;

        //Inventory
        this.inventory = FindObjectOfType<InventoryManager>();
    }

    protected override void Start()
    {
        base.Start();
        //Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;
        this.SpawnWeapon(this.currentStartingWeapon);
        this.SpawnWeapon(this.weaponTest2);
        this.SpawnPassiveItem(this.passiveItemTest1);
        this.SpawnPassiveItem(this.passiveItemTest2);
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

    public virtual void TakeDamage(float amount)
    {
        //If the player is not currently invincible, reduce health and start invincibility 
        if (!isInvincible)
        {
            this.currentHealth -= amount;
            //UIHPBar.Instance.UpdateHpBar();

            this.invincibilityTimer = this.invincibilityDuration;
            this.isInvincible= true;

            if (this.currentHealth <= 0)
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
        Debug.LogWarning("Player is DEAD");
    }

    public virtual void RestoreHealth(float health)
    {
        if (this.currentHealth >= this.characterData.MaxHealth) return;
        this.currentHealth += health;
        if(this.currentHealth > this.characterData.MaxHealth)
        {
            this.currentHealth = this.characterData.MaxHealth;
        }
    }

    protected virtual void Recover()
    {
        if (this.currentHealth >= this.characterData.MaxHealth) return;
        this.currentHealth += this.currentRecovery * Time.deltaTime;
        if(this.currentHealth > this.characterData.MaxHealth)
        {
            this.currentHealth = this.characterData.MaxHealth;
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
                Debug.Log("Checked");
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
