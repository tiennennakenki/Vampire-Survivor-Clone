using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SaiMonoBehaviour
{
    public List<WeaponSpawner> weaponSlots = new List<WeaponSpawner>(6);
    public List<Image> weaponUISlots = new List<Image>(6);
    public int[] weaponLevels = new int[6];
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public List<Image> passiveItemUISlots = new List<Image>(6);
    public int[] passiveItemLevels = new int[6];

    public virtual void AddWeapon(int indexSlot, WeaponSpawner weapon) //Add a weapon to a specific slot
    {
        this.weaponSlots[indexSlot] = weapon;
        this.weaponLevels[indexSlot] = weapon.weaponData.Level;
        this.weaponUISlots[indexSlot].enabled = true;
        this.weaponUISlots[indexSlot].sprite = weapon.weaponData.Icon;
    }

    public virtual void AddPassiveItem(int indexSlot, PassiveItem passiveItem) //Add a PassiveItem to a specific slot
    {
        this.passiveItemSlots[indexSlot] = passiveItem;
        this.passiveItemLevels[indexSlot] = passiveItem.passiveItemData.Level;
        this.passiveItemUISlots[indexSlot].enabled = true;
        this.passiveItemUISlots[indexSlot].sprite = passiveItem.passiveItemData.Icon; 
    }

    public virtual void LevelUpWeapon(int indexSlot)
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
    }

    public virtual void LevelUpPassiveItem(int indexSlot)
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
    }
}
