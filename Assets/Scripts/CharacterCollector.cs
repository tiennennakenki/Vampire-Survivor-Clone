using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollector : SaiMonoBehaviour
{
    private static CharacterCollector instance;
    public static CharacterCollector Instance => instance;
    public CharacterData characterData;
    [SerializeField] protected TextMeshProUGUI characterNameGUI;
    [SerializeField] protected TextMeshProUGUI characterDescriptionGUI;
    [SerializeField] protected Image characterAvatarInfo;
    [SerializeField] protected Image characterAvatarIsSuccessfullyPurchased;
    [SerializeField] protected Image characterAvatarIsFailedPurchased;
    [SerializeField] protected Image iconSkill;
    [SerializeField] protected string characterName;

    [Header("Confirm/Buy")]
    [SerializeField] protected GameObject confirmButton;
    [SerializeField] protected GameObject purchaseScreen;
    [SerializeField] protected GameObject characterSelectionScreen;
    [SerializeField] protected GameObject successfullyPurchaseScreen;
    [SerializeField] protected GameObject failedPurchaseScreen;
    [SerializeField] protected TextMeshProUGUI priceCharacterText;
    [SerializeField] protected TextMeshProUGUI gameNameText;
    [SerializeField] protected float priceCharacter;

    // Create an event to notify when a character is purchased
    public static event System.Action<CharacterData> OnCharacterBought;

    protected override void Awake()
    {
        base.Awake();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Only 1 CharacterCollector allow to exits");
            Destroy(gameObject);
        }
    }

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterNameGUI();
        this.LoadCharacterDescriptionGUI();
        this.LoadCharacterAvatar();
        this.LoadIconSkill();
        this.LoadCharacterAvatarInfo();
        this.LoadCharacterAvatarIsSuccessfullyPurchased();
        this.LoadCharacterAvatarIsFailedPurchased();
        this.LoadConfirmButton();
        this.LoadPurchaseScreen();
        this.LoadCharacterSelectionScreen();
        this.LoadSuccessfullyPurchaseScreen();
        this.LoadFailedPurchaseScreen();
        this.LoadPriceCharacterText();
        this.LoadGameNameText();
    }

    protected virtual void LoadCharacterNameGUI()
    {
        if (this.characterNameGUI != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");
        Transform info = characterSelectionScreen.Find("Info");
        this.characterNameGUI = info.Find("Character Name").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadCharacterNameGUI", gameObject);
    }

    protected virtual void LoadCharacterDescriptionGUI()
    {
        if (this.characterDescriptionGUI != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");
        Transform info = characterSelectionScreen.Find("Info");
        this.characterDescriptionGUI = info.Find("Description").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadCharacterNameGUI", gameObject);
    }

    protected virtual void LoadCharacterAvatar()
    {
        if (this.characterAvatarInfo != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");
        Transform info = characterSelectionScreen.Find("Info");
        this.characterAvatarInfo = info.Find("Avatar").GetComponent<Image>();

        Debug.LogWarning(transform.name + ": LoadCharacterAvatar", gameObject);
    }

    protected virtual void LoadIconSkill()
    {
        if (this.iconSkill != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");
        Transform info = characterSelectionScreen.Find("Info");
        Transform skill = info.Find("Skill");
        this.iconSkill = skill.Find("UI Avatar").GetComponent<Image>();

        Debug.LogWarning(transform.name + ": LoadIconSkill", gameObject);
    }

    protected virtual void LoadCharacterAvatarInfo()
    {
        if (this.characterAvatarInfo != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");
        Transform info = characterSelectionScreen.Find("Info");

        this.characterAvatarInfo = info.Find("Avatar").GetComponent<Image>();

        Debug.LogWarning(transform.name + ": LoadCharacterAvatarInfo", gameObject);
    }

    protected virtual void LoadCharacterAvatarIsSuccessfullyPurchased()
    {
        if (this.characterAvatarIsSuccessfullyPurchased != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform successfullyPurchaseScreen = screen.Find("Successfully Purchase Screen");
        Transform avatarCharacter = successfullyPurchaseScreen.Find("Avatar Character");

        this.characterAvatarIsSuccessfullyPurchased = avatarCharacter.Find("Avatar Character").GetComponent<Image>();

        Debug.LogWarning(transform.name + ": CharacterAvatarIsSuccessfullyPurchased", gameObject);
    }

    protected virtual void LoadCharacterAvatarIsFailedPurchased()
    {
        if (this.characterAvatarIsFailedPurchased != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform failedPurchaseScreen = screen.Find("Failed Purchase Screen");
        Transform avatarCharacter = failedPurchaseScreen.Find("Avatar Character");

        this.characterAvatarIsFailedPurchased = avatarCharacter.Find("Avatar Character").GetComponent<Image>();

        Debug.LogWarning(transform.name + ": CharacterAvatarIsFailedPurchased", gameObject);
    }

    protected virtual void LoadConfirmButton()
    {
        if (this.confirmButton != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");

        this.confirmButton = characterSelectionScreen.Find("Confirm Btn").GetComponent<Button>().gameObject;

        Debug.LogWarning(transform.name + ": LoadConfirmButton", gameObject);
    }

    protected virtual void LoadPurchaseScreen()
    {
        if (this.purchaseScreen != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");

        this.purchaseScreen = characterSelectionScreen.Find("Purchase Screen").gameObject;

        Debug.LogWarning(transform.name + ": LoadPurchaseScreen", gameObject);
    }

    protected virtual void LoadCharacterSelectionScreen()
    {
        if (this.characterSelectionScreen != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");

        this.characterSelectionScreen = screen.Find("Character Selection Screen").gameObject;

        Debug.LogWarning(transform.name + ": LoadCharacterSelectionScreen", gameObject);
    }

    protected virtual void LoadSuccessfullyPurchaseScreen()
    {
        if (this.successfullyPurchaseScreen != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");

        this.successfullyPurchaseScreen = screen.Find("Successfully Purchase Screen").gameObject;

        Debug.LogWarning(transform.name + ": LoadSuccessfullyPurchaseScreen", gameObject);
    }

    protected virtual void LoadFailedPurchaseScreen()
    {
        if (this.failedPurchaseScreen != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");

        this.failedPurchaseScreen = screen.Find("Failed Purchase Screen").gameObject;

        Debug.LogWarning(transform.name + ": LoadFailedPurchaseScreen", gameObject);
    }

    protected virtual void LoadPriceCharacterText()
    {
        if (this.priceCharacterText != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform screen = canvas.Find("Screen");
        Transform characterSelectionScreen = screen.Find("Character Selection Screen");
        Transform purchaseScreen = characterSelectionScreen.Find("Purchase Screen");

        this.priceCharacterText = purchaseScreen.Find("Price Purchase").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadPriceCharacterText", gameObject);
    }

    protected virtual void LoadGameNameText()
    {
        if (this.gameNameText != null) return;
        Transform canvas = FindObjectOfType<Canvas>().transform;

        this.gameNameText = canvas.Find("Game Name Text").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadGameNameText", gameObject);
    }

    #endregion

    public CharacterData GetData()
    {
        if (instance && instance.characterData)
        {
            return instance.characterData;
        }
        else
        {
            // Randomly pick a character if we are playing from the Editor.
            #if UNITY_EDITOR
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            List<CharacterData> characters = new List<CharacterData>();
            foreach (string assetPath in allAssetPaths)
            {
                if (assetPath.EndsWith(".asset"))
                {
                    CharacterData characterData = AssetDatabase.LoadAssetAtPath<CharacterData>(assetPath);
                    if (characterData != null)
                    {
                        characters.Add(characterData);
                    }
                }
            }

            // Pick a random character if we have found any characters.
            if (characters.Count > 0)
            {
                Debug.LogWarning($"{characters.Count} characters");
                return characters[Random.Range(0, characters.Count)];
            }
            #endif

            //// If no character data is assigned, we randomly pick one.
            //CharacterData[] characters = Resources.FindObjectsOfTypeAll<CharacterData>();
            //if (characters.Length > 0)
            //{
            //    return characters[Random.Range(0, characters.Length)];
            //}
        }
        Debug.LogWarning("null");
        return null;
    }

    public virtual void SelectCharacter(CharacterData character)
    {
        this.characterData = character;
        CharacterCollector.OnCharacterBought += UpdateCharacterAvatar;

        this.characterNameGUI.SetText(this.characterData.Name);
        this.characterDescriptionGUI.SetText(this.characterData.Description);
        this.characterAvatarInfo.sprite = this.characterData.Icon;
        this.iconSkill.sprite = this.characterData.StartingWeapon.icon;

        this.priceCharacter = this.characterData.Price;
        this.characterName = this.characterData.Name;
        //Debug.Log("select character successfull");
        //Debug.Log(this.characterName);
        
        this.UpdateCharacterAvatar(characterData);
    }

    protected virtual void UpdateCharacterAvatar(CharacterData characterData)
    {
        if (characterData.IsBought) //Display Confirm UI
        {
            this.SetWhiteColor(characterAvatarInfo);
            EnableConfirmUI();
        }
        else //Display Buy UI
        {
            this.SetBlackColor(characterAvatarInfo);
            this.priceCharacterText.text = this.priceCharacter.ToString();
            EnableBuyUI();
        }
    }

    public virtual void SelectedCharacter()
    {
        PlayerPrefs.SetString("SelectedCharacter", this.characterName.ToString());
        PlayerPrefs.Save();
    }

    public virtual void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
    
    protected virtual void EnableConfirmUI()
    {
        this.confirmButton.SetActive(true);
        this.purchaseScreen.SetActive(false);
    }

    protected virtual void EnableBuyUI()
    {
        this.purchaseScreen.SetActive(true);
        this.confirmButton.SetActive(false);
    }

    public virtual void BuyCharacter()
    {
        float totalCoin = PlayerPrefs.GetFloat("TotalCoin");

        if(totalCoin < this.priceCharacter)
        {
            this.FailedPurchase();
            return;
        }

        totalCoin -= this.priceCharacter;
        PlayerPrefs.SetFloat("TotalCoin", totalCoin);
        PlayerPrefs.Save();

        this.characterData.IsBought = true;
        this.SuccessfullyPurchase();

        OnCharacterBought?.Invoke(characterData);
    }

    protected virtual void SuccessfullyPurchase()
    {
        if (this.successfullyPurchaseScreen == null) return;

        this.characterAvatarIsSuccessfullyPurchased.sprite = this.characterAvatarInfo.sprite;
        this.SetWhiteColor(characterAvatarIsSuccessfullyPurchased);

        this.gameNameText.gameObject.SetActive(false);
        this.characterSelectionScreen.SetActive(false);
        this.failedPurchaseScreen.SetActive(false);
        this.successfullyPurchaseScreen.SetActive(true);

        SoundController.Instance.PlayItemUnlockedSoundEffect();
    }

    protected virtual void FailedPurchase()
    {
        if (this.failedPurchaseScreen == null) return;

        this.characterAvatarIsFailedPurchased.sprite = this.characterAvatarInfo.sprite;
        SetBlackColor(characterAvatarIsFailedPurchased);

        this.gameNameText.gameObject.SetActive(false);
        this.characterSelectionScreen.SetActive(false);
        this.successfullyPurchaseScreen.SetActive(false);
        this.failedPurchaseScreen.SetActive(true);

        SoundController.Instance.PlayItemFailedSoundEffect();
    }

    protected virtual void SetBlackColor(Image image)
    {
        image.color = Color.black;
    }

    protected virtual void SetWhiteColor(Image image)
    {
        image.color = Color.white;
    }
}
