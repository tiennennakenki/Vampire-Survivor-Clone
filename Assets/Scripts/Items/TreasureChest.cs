using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : SaiMonoBehaviour
{
    [SerializeField] protected float coin;
    [SerializeField] protected TextMeshProUGUI coinText;
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected Button openBtn;
    [SerializeField] protected Image iconWeapon;

    protected override void Start()
    {
        coin = Random.Range(5, 200);
        this.LoadComponents();
    }

    #region LoadComponents 
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadCoinText();
        this.LoadOpenBtn();
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
        if (this.coinText != null) return;

        if (canvas == null) return;

        Transform screen = canvas.transform.Find("Screens");

        Transform treasureChestScreen = screen.Find("Treasure Chest Screen");

        Transform coin = treasureChestScreen.Find("Coin");
        this.coinText = coin.Find("Coin Text").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + " : LoadCoinText", gameObject);
    }


    protected virtual void LoadOpenBtn()
    {
        if (this.openBtn != null) return;
        if (canvas == null) return;

        Transform screen = canvas.transform.Find("Screens");
        Transform treasureChestScreen = screen.Find("Treasure Chest Screen");
        this.openBtn = treasureChestScreen.Find("Open Btn").GetComponent<Button>();

        Debug.LogWarning(transform.name + " : LoadOpenBtn", gameObject);
    }

    protected virtual void LoadIconWeapon()
    {
        if (this.iconWeapon != null) return;
        if (canvas == null) return;

        Transform screen = canvas.transform.Find("Screens");
        Transform treasureChestScreen = screen.Find("Treasure Chest Screen");
        this.iconWeapon = treasureChestScreen.Find("Icon Weapon").GetComponent<Image>();

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
            openBtn.onClick.AddListener(() => this.UpdateCoin());
        }
    }

    public void OpenTreasureChest(PlayerInventory inventory, bool isHigherTier)
    {
        GameManager.Instance.OpenTreasureChest();

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
                    if (attempt) return true; // If evolution suceeds, stop.
                }

            }
        }

        return false;
    }

    protected virtual void LevelUpWeapon(PlayerInventory inventory)
    {
        int slots = 0;
        List<Weapon> validWeapons = new List<Weapon>();

        foreach (PlayerInventory.Slot s in inventory.weaponSlots)
        {
            Weapon w = s.item as Weapon;
            if (w == null) continue;

            Weapon.Stats nextLevel = w.data.GetLevelData(w.currentLevel + 1);
            if (nextLevel.Equals(default(Weapon.Stats))) continue;

            validWeapons.Add(w);
            slots++;
        }

        if (validWeapons.Count > 0)
        {
            Weapon weapon = validWeapons[Random.Range(0, validWeapons.Count)];
            weapon.DoLevelUp();
            iconWeapon.sprite = weapon.data.icon;
        }

        return;
    }

    public virtual void UpdateCoin()
    {
        if (coin != 0)
        {
            float coinClone = Random.Range(1, coin);
            coinClone = (float)System.Math.Round(coinClone, 2);

            //StartCoroutine(UpdateCoin(coinClone));

            this.coinText.text = coinClone.ToString();
            GameManager.Instance.IncreaseCoin(coinClone);
            Debug.Log("Coin: " + coinClone);
        }

        Destroy(gameObject);
    }

    //public virtual IEnumerator UpdateCoin(float finalCoin)
    //{
    //    float currentCoin = 0;
    //    float increment = 0.01f;
    //    float delay = 0.00005f;

    //    while (currentCoin < finalCoin)
    //    {
    //        currentCoin += increment;
    //        if (currentCoin > finalCoin)
    //        {
    //            currentCoin = finalCoin;
    //        }

    //        this.coinText.text = currentCoin.ToString("F2");

    //        yield return new WaitForSeconds(delay);
    //    }

    //    GameManager.Instance.IncreaseCoin(finalCoin);
    //    Debug.Log("Coin: " + finalCoin);
        
    //}
}