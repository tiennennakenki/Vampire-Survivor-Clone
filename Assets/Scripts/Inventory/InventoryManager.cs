using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SaiMonoBehaviour
{
    [Header("Inventory Manager")]
    public List<WeaponSpawner> weaponSlots = new List<WeaponSpawner>(6);
    public List<Image> weaponUISlots = new List<Image>(6);
    public int[] weaponLevels = new int[6];
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public List<Image> passiveItemUISlots = new List<Image>(6);
    public int[] passiveItemLevels = new int[6];

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>(); //List of upgrade options for weapons
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>(); //List of upgrade options for passiveItems
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>(); //List of upgrade options for upgradeUI

    public PlayerStats player;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerStats>();
    }

    public virtual void AddWeapon(int indexSlot, WeaponSpawner weapon) //Add a weapon to a specific slot
    {
        this.weaponSlots[indexSlot] = weapon;
        this.weaponLevels[indexSlot] = weapon.weaponData.Level;
        this.weaponUISlots[indexSlot].enabled = true;
        this.weaponUISlots[indexSlot].sprite = weapon.weaponData.Icon;

        if(GameManager.Instance != null  && GameManager.Instance.choosingUpgrade)
        {
            GameManager.Instance.EndLevelUP();
        }
    }

    public virtual void AddPassiveItem(int indexSlot, PassiveItem passiveItem) //Add a PassiveItem to a specific slot
    {
        this.passiveItemSlots[indexSlot] = passiveItem;
        this.passiveItemLevels[indexSlot] = passiveItem.passiveItemData.Level;
        this.passiveItemUISlots[indexSlot].enabled = true;
        this.passiveItemUISlots[indexSlot].sprite = passiveItem.passiveItemData.Icon;

        if (GameManager.Instance != null && GameManager.Instance.choosingUpgrade)
        {
            GameManager.Instance.EndLevelUP();
        }
    }

    public virtual void LevelUpWeapon(int indexSlot, int upgradeIndex)
    {
        if (this.weaponSlots.Count <= indexSlot) return;
        WeaponSpawner weapon = weaponSlots[indexSlot];

        if (!weapon.weaponData.NextLevelPrefab)
        {
            Debug.LogError("NOT NEXT LEVEL FOR" + weapon.name);
        }

        Destroy(weapon.gameObject);
        GameObject upgradeWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
        upgradeWeapon.transform.SetParent(transform);
        upgradeWeapon.SetActive(true);
        AddWeapon(indexSlot, upgradeWeapon.GetComponent<WeaponSpawner>());
        weaponLevels[indexSlot] = upgradeWeapon.GetComponent<WeaponSpawner>().weaponData.Level;

        weaponUpgradeOptions[upgradeIndex].weaponData = upgradeWeapon.GetComponent<WeaponSpawner>().weaponData;

        if (GameManager.Instance != null && GameManager.Instance.choosingUpgrade)
        {
            GameManager.Instance.EndLevelUP();
        }
    }

    public virtual void LevelUpPassiveItem(int indexSlot, int upgradeIndex)
    {
        if (this.passiveItemSlots.Count <= indexSlot) return;
        PassiveItem passiveItem = passiveItemSlots[indexSlot];

        if (!passiveItem.passiveItemData.NextLevelPrefab)
        {
            Debug.LogError("NOT NEXT LEVEL FOR" + passiveItem.name);
        }

        GameObject upgradePassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
        upgradePassiveItem.transform.SetParent(transform);
        AddPassiveItem(indexSlot, upgradePassiveItem.GetComponent<PassiveItem>());
        Destroy(passiveItem.gameObject);
        passiveItemLevels[indexSlot] = upgradePassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;

        passiveItemUpgradeOptions[upgradeIndex].passiveItemData = upgradePassiveItem.GetComponent<PassiveItem>().passiveItemData;

        if (GameManager.Instance != null && GameManager.Instance.choosingUpgrade)
        {
            GameManager.Instance.EndLevelUP();
        }
    }

    protected virtual void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availbleWeaponUpgrade = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<PassiveItemUpgrade> availblePassiveItemUpgrade = new List<PassiveItemUpgrade>(passiveItemUpgradeOptions);

        foreach (UpgradeUI upgradeOption in upgradeUIOptions)
        {
            if (availbleWeaponUpgrade.Count == 0 && availblePassiveItemUpgrade.Count == 0) return;

            int upgradeType;
            if (availbleWeaponUpgrade.Count == 0)
            {
                upgradeType = 2;
            }
            else if(availblePassiveItemUpgrade.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);
            }

            //Implementation
            if (upgradeType == 1)
            {
                this.WeaponsUpgrade(upgradeOption, availbleWeaponUpgrade);
            }
            else if(upgradeType == 2)
            {
                this.PassiveItemsUpgrade(upgradeOption, availblePassiveItemUpgrade);
            }
        }
    }

    protected virtual void WeaponsUpgrade(UpgradeUI upgradeOption, List<WeaponUpgrade> availbleWeaponUpgrade)
    {
        WeaponUpgrade chosenWeaponUpgrade = availbleWeaponUpgrade[Random.Range(0, availbleWeaponUpgrade.Count)];
        availbleWeaponUpgrade.Remove(chosenWeaponUpgrade);

        if (chosenWeaponUpgrade == null) return; //If don't find out weapon Upgrade then return

        EnableUpgradeUI(upgradeOption);
        bool newWeapon = false;
        for (int i = 0; i < this.weaponSlots.Count; i++)
        {
            if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
            {
                newWeapon = false;
                if (newWeapon) break;
                if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab)
                {
                    this.DisableUpgradeUI(upgradeOption);
                    break;
                }
                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex)); //Apply button functionality

                //Set the description and name to be that of next level
                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponSpawner>().weaponData.Name;
                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponSpawner>().weaponData.Description;
                break;
            }
            else
            {
                newWeapon = true;
            }
        }
        if (!newWeapon) return;
        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon)); //Apply button functionality

        //Set the description and name to be that of level
        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.Name;
        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;

        upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon; 
    }

    protected virtual void PassiveItemsUpgrade(UpgradeUI upgradeOption, List<PassiveItemUpgrade> availblePassiveItemUpgrade)
    {
        PassiveItemUpgrade chosenPassiveItemUpgrade = availblePassiveItemUpgrade[Random.Range(0, availblePassiveItemUpgrade.Count)];
        availblePassiveItemUpgrade.Remove(chosenPassiveItemUpgrade);

        if (chosenPassiveItemUpgrade == null) return;

        EnableUpgradeUI(upgradeOption);
        bool newWeapon = false;
        for (int i = 0; i < this.passiveItemSlots.Count; i++)
        {
            if (passiveItemSlots[i] != null && passiveItemSlots[i].passiveItemData == chosenPassiveItemUpgrade.passiveItemData)
            {
                newWeapon = false;
                if (newWeapon) break;
                if (!chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab)
                {
                    this.DisableUpgradeUI(upgradeOption);
                    break;
                }
                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i, chosenPassiveItemUpgrade.passiveItemUpgradeIndex)); //Apply button functionality
                
                //Set the description and name to be that of next level
                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Name;
                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Description;
                break;
            }
            else
            {
                newWeapon = true;
            }
        }
        if (!newWeapon) return;
        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemUpgrade.initialPassiveItem)); //Apply button functionality
        
        //Set the description and name to be that of level
        upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Name;
        upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Description;

        upgradeOption.upgradeIcon.sprite = chosenPassiveItemUpgrade.passiveItemData.Icon;
    }

    protected virtual void RemoveUpgradeOptions()
    {
        foreach(UpgradeUI upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            this.DisableUpgradeUI(upgradeOption); //Call DisableUpgradeUI method here to disable all UI before appling upgrades to them
        }
    }

    public virtual void RemoveAndApplyUpgrades()
    {
        this.RemoveUpgradeOptions();
        this.ApplyUpgradeOptions();
    }

    protected virtual void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }

    protected virtual void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }
}
