using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TreasureChest : SaiMonoBehaviour
{
    [SerializeField] protected float coin;
    [SerializeField] protected TextMeshProUGUI coinText;
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected Button openBtn;
    [SerializeField] protected Button closeBtn;
    [SerializeField] protected Image iconWeapon;
    [SerializeField] protected bool isOpened = false;

    protected override void Awake()
    {
        this.LoadComponents();
        this.ResetCoin();
    }

    protected override void OnEnable()
    {
        this.ResetCoin();
        this.isOpened = false;
    }

    protected virtual void ResetCoin()
    {
        coin = Random.Range(5, 200);
    }

    #region LoadComponents 
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadCoinText();
        this.LoadOpenBtn();
        this.LoadCloseBtn();
        this.LoadIconWeapon();
    }

    protected virtual void LoadCanvas()
    {
        Canvas[] allCanvas = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvas)
        {
            if (canvas.CompareTag("UIGame")) 
            {
                this.canvas = canvas;
                return;
            }
        }

        if (canvas == null)
        {
            Debug.LogError("Canvas not found");
        }
    }

    protected virtual void LoadCoinText()
    {
        if (canvas == null) return;
        if (this.coinText != null) return;

        Transform screen = canvas.transform.Find("Screens");

        Transform treasureChestScreen = screen.Find("Treasure Chest Screen");
        Transform takeCoinScreen = treasureChestScreen.Find("Take Coin Screen");
        Transform coin = takeCoinScreen.Find("Coin");
        this.coinText = coin.Find("Coin Text").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + " : LoadCoinText", gameObject);
    }


    protected virtual void LoadOpenBtn()
    {
        if (canvas == null) return;
        if (this.openBtn != null) return;

        Transform screen = canvas.transform.Find("Screens");
        Transform treasureChestScreen = screen.Find("Treasure Chest Screen");
        this.openBtn = treasureChestScreen.Find("Open Btn").GetComponent<Button>();

        Debug.LogWarning(transform.name + " : LoadOpenBtn", gameObject);
    }
    protected virtual void LoadCloseBtn()
    {
        if (canvas == null) return;
        if (this.closeBtn != null) return;

        Transform screen = canvas.transform.Find("Screens");
        Transform treasureChestScreen = screen.Find("Treasure Chest Screen");
        Transform takeWeaponScreen = treasureChestScreen.Find("Take Weapon Screen");
        this.closeBtn = takeWeaponScreen.Find("Close Btn").GetComponent<Button>();

        Debug.LogWarning(transform.name + " : LoadCloseBtn", gameObject);
    }

    protected virtual void LoadIconWeapon()
    {
        if (canvas == null) return;
        if (this.iconWeapon != null) return;

        Transform screen = canvas.transform.Find("Screens");
        Transform treasureChestScreen = screen.Find("Treasure Chest Screen");
        Transform takeWeaponScreen = treasureChestScreen.Find("Take Weapon Screen");
        Transform icon = takeWeaponScreen.Find("Icon");
        this.iconWeapon = icon.Find("Icon Weapon").GetComponent<Image>();

        Debug.LogWarning(transform.name + " : LoadIconWeapon", gameObject);
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerInventory p = col.GetComponent<PlayerInventory>();
        if (p)
        {
            bool randomBool = Random.Range(0, 2) == 0;

            OpenTreasureChest(p, randomBool);

            if (!this.isOpened) return;
            this.openBtn.onClick.AddListener(() => this.UpdateCoin());
            this.closeBtn.onClick.AddListener(() => this.DespawnTreasureChest());
        }
    }

    public void OpenTreasureChest(PlayerInventory inventory, bool isHigherTier)
    {
        GameManager.Instance.OpenTreasureChest();
        this.isOpened = true;

        if (this.EvolutionWeapon(inventory)) return;
        this.LevelUpWeapon(inventory);
    }

    protected virtual bool EvolutionWeapon(PlayerInventory inventory)
    {
        // Loop through every weapon to check whether it can evolve.
        foreach (PlayerInventory.Slot s in inventory.weaponSlots)
        {
            Weapon w = s.item as Weapon;
            if (w == null) continue;
            if (w.data.evolutionData == null) continue; // Ignore weapon if it cannot evolve.

            // Loop through every possible evolution of the weapon.
            foreach (ItemData.Evolution e in w.data.evolutionData)
            {
                // Only attempt to evolve weapons via treasure chest evolution.
                if (e.condition == ItemData.Evolution.Condition.treasureChest)
                {
                    bool attempt = w.AttemptEvolution(e, 0);
                    if (attempt) 
                    {
                        iconWeapon.sprite = w.data.icon;
                        return true; // If evolution suceeds, stop.
                    }
                }
            }
        }

        return false;
    }

    protected virtual void LevelUpWeapon(PlayerInventory inventory)
    {
        List<Weapon> validWeapons = new List<Weapon>();

        foreach (PlayerInventory.Slot s in inventory.weaponSlots)
        {
            Weapon w = s.item as Weapon;
            if (w == null) continue;

            Weapon.Stats nextLevel = w.data.GetLevelData(w.currentLevel + 1);
            if (nextLevel.Equals(default(Weapon.Stats))) continue;

            validWeapons.Add(w);
        }

        if (validWeapons.Count > 0)
        {
            Weapon weapon = validWeapons[Random.Range(0, validWeapons.Count)];
            if (weapon.DoLevelUp())
            {
                iconWeapon.sprite = weapon.data.icon;
            }
        }

        foreach (PlayerInventory.Slot s in inventory.weaponSlots)
        {
            Weapon w = s.item as Weapon;
            if (w == null) continue;

            s.level.text = w.GetStats().level.ToString();
        }

        return;
    }

    public virtual void UpdateCoin()
    {
        if (coin != 0)
        {
            float coinClone = Random.Range(1, coin);
            coinClone = (float)System.Math.Round(coinClone, 2);

            this.coinText.text = coinClone.ToString();
            GameManager.Instance.IncreaseCoin(coinClone);
        }
    }

    public virtual void DespawnTreasureChest()
    {
        if (!this.isOpened) return;
        ItemsDropSpawner.Instance.Despawn(this.gameObject.transform);
        this.isOpened = false;
    }
}