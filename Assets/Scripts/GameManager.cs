using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SaiMonoBehaviour
{
    [Header("Game Manager")]
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] protected Canvas canvas;

    [SerializeField] protected GameState currentState;
    [SerializeField] protected GameState previousState;

    //Screens
    [Header("Screens")]
    [SerializeField] protected GameObject pauseScreen;
    [SerializeField] protected GameObject resultsScreen;
    [SerializeField] protected GameObject levelUpScreen;
    [SerializeField] protected GameObject treasureChestScreen;
    [SerializeField] protected GameObject gameOverScreen;

    //Current Stat Display
    [Header("Results Screen Displays")]
    [SerializeField] protected Image chosenCharacterImage;
    [SerializeField] protected TextMeshProUGUI chosenCharacterName;
    [SerializeField] protected TextMeshProUGUI levelReachedDisplay;
    [SerializeField] protected TextMeshProUGUI timeSurvied;
    [SerializeField] protected TextMeshProUGUI totalCoin;
    [SerializeField] protected TextMeshProUGUI totalEnemiesDead;
    [SerializeField] protected List<Image> chosenWeaponsUI = new List<Image>(6);
    [SerializeField] protected List<Image> chosenPassiveItemsUI = new List<Image>(6);

    [Header("Damage Text Setting")]
    //public Canvas damageTextCanvas;
    public float textFontsize = 20f;
    public TMP_FontAsset textFont;
    public Camera referenceCamera;

    //Flag to check if the game is over
    public bool isGameOver = false;

    //Flag to check if the player is choosing their upgrades
    public bool choosingUpgrade;

    //Reference to the player's game object
    public PlayerInventory playerInventory;
    public PlayerStats playerStats;

    //Stopwatch UI
    [Header("Stopwatch")]
    //[SerializeField] protected float timeLimit; //The time limit in seconds
    [SerializeField] protected float stopwatchTime; //The current time eslaped since the stopwatch started
    [SerializeField] protected TextMeshProUGUI stopwatchDisplay;

    [Header("Coin/Enemies")]
    public float coin = 0;
    public int enemiesDead = 0;
    [SerializeField] public TextMeshProUGUI coinText;
    [SerializeField] public TextMeshProUGUI enemiesDeadText;


    [SerializeField] protected TextMeshProUGUI damageFloatingText;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 GameManager allow to exits");
        instance = this;
        this.DisableScreen();
    }

    protected override void OnEnable()
    {
        PlayerSelection.CharacterSetEvent += LoadPlayer;
    }

    protected override void OnDisable()
    {
        PlayerSelection.CharacterSetEvent -= LoadPlayer;
    }

    protected virtual void LoadPlayer()
    {
        this.playerInventory = PlayerCtrl.Instance.Model.GetComponent<PlayerInventory>();
        if (playerInventory == null) Debug.LogError("PlayerInventory not found");
        this.playerStats = PlayerCtrl.Instance.Model.GetComponent<PlayerStats>();
        
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadPauseScreen();
        this.LoadResultsScreen();
        this.LoadLevelUpScreen();
        this.LoadTreasureChestScreen();
        this.LoadGameOverScreen();
        this.LoadChosenCharacterImage();
        this.LoadChosenCharacterName();
        this.LoadLevelReachedDisplay();
        this.LoadTimeSurvivedDisplay();
        this.LoadTotalCoinDisplay();
        this.LoadTotalEnemiesDeadDisplay();
        this.LoadListChosenWeaponsUI();
        this.LoadListChosenPassiveItemsUI();
        this.LoadStopwatchDisplay();
        this.LoadCoinText();
        this.LoadEnemiesDeadText();
        this.LoadreferenceCamera();
        //this.LoadDamageFloatingText();
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

    protected virtual void LoadPauseScreen()
    {
        if (this.pauseScreen != null) return;
        if (this.canvas == null) return;
        Transform screens = canvas.transform.Find("Screens");
        this.pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        Debug.LogWarning(transform.name + ": LoadPauseScreen", gameObject);
    }

    protected virtual void LoadCoinText()
    {
        if (this.canvas == null) return;
        if (this.coinText != null) return;
        Transform coinDisplay = canvas.transform.Find("Coin Display");
        this.coinText = coinDisplay.transform.Find("Total Coin").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCoinText", gameObject);
    }

    protected virtual void LoadDamageFloatingText()
    {
        if (this.canvas == null) return;
        if (this.damageFloatingText != null) return;
        this.coinText = damageFloatingText.transform.Find("Damage Floating Text").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadDamageFloatingText", gameObject);
    }

    protected virtual void LoadreferenceCamera()
    {
        if (this.referenceCamera != null) return;

        this.referenceCamera = FindObjectOfType<Camera>();
        Debug.LogWarning(transform.name + ": LoadCoinText", gameObject);
    }

    protected virtual void LoadEnemiesDeadText()
    {
        if (this.canvas == null) return;
        if (this.enemiesDeadText != null) return;
        Transform enemiesDeadDisplay = canvas.transform.Find("Enemies Dead Display");
        this.enemiesDeadText = enemiesDeadDisplay.transform.Find("Total Enemies Dead").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadEnemiesDeadText", gameObject);
    }

    protected virtual void LoadResultsScreen()
    {
        if (this.resultsScreen != null) return;
        if (this.canvas == null) return;
        Transform screens = canvas.transform.Find("Screens");
        this.resultsScreen = screens.transform.Find("Results Screen").gameObject;
        Debug.LogWarning(transform.name + ": LoadPauseScreen", gameObject);
    }

    protected virtual void LoadLevelUpScreen()
    {
        if (this.levelUpScreen != null) return;

        if (this.canvas == null) return;
        Transform screens = canvas.transform.Find("Screens");
        this.levelUpScreen = screens.transform.Find("Level Up Screen").gameObject;

        Debug.LogWarning(transform.name + ": LoadLevelUpScreen", gameObject);
    }

    protected virtual void LoadTreasureChestScreen()
    {
        if (this.treasureChestScreen != null) return;
        if (this.canvas == null) return;
        Transform screens = canvas.transform.Find("Screens");
        this.treasureChestScreen = screens.transform.Find("Treasure Chest Screen").gameObject;
        Debug.LogWarning(transform.name + ": LoadTreasureChestScreen", gameObject);
    }

    protected virtual void LoadGameOverScreen()
    {
        if (this.gameOverScreen != null) return;
        if (this.canvas == null) return;
        Transform screens = canvas.transform.Find("Screens");
        this.gameOverScreen = screens.transform.Find("Game Over Screen").gameObject;
        Debug.LogWarning(transform.name + ": LoadGameOverScreen", gameObject);
    }

    protected virtual void LoadChosenCharacterImage()
    {
        if (this.canvas == null) return;
        if (this.chosenCharacterImage != null) return;
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject resultsScreen = screens.transform.Find("Results Screen").gameObject;
        GameObject uiResults = resultsScreen.transform.Find("UI Results").gameObject;
        GameObject chosenCharacterHolder = uiResults.transform.Find("Chosen Character Holder").gameObject;
        this.chosenCharacterImage = chosenCharacterHolder.transform.Find("Chosen Character Image").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadChosenCharacterImage", gameObject);
    }

    protected virtual void LoadChosenCharacterName()
    {
        if (this.canvas == null) return;
        if (this.chosenCharacterName != null) return;
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject resultsScreen = screens.transform.Find("Results Screen").gameObject;
        GameObject uiResults = resultsScreen.transform.Find("UI Results").gameObject;
        GameObject chosenCharacterHolder = uiResults.transform.Find("Chosen Character Holder").gameObject;
        this.chosenCharacterName = chosenCharacterHolder.transform.Find("Chosen Character Name").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadChosenCharacterName", gameObject);
    }

    protected virtual void LoadLevelReachedDisplay()
    {
        if (this.canvas == null) return;
        if (this.levelReachedDisplay != null) return;
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject resultsScreen = screens.transform.Find("Results Screen").gameObject;
        GameObject uiResults = resultsScreen.transform.Find("UI Results").gameObject;
        GameObject levelReachedHolder = uiResults.transform.Find("Level Reached Holder").gameObject;
        this.levelReachedDisplay = levelReachedHolder.transform.Find("Level Reached Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadLevelReachedDisplay", gameObject);
    }

    protected virtual void LoadListChosenWeaponsUI()
    {
        if (this.canvas == null) return;
        if (this.chosenWeaponsUI.Count > 0) return;

        Transform screens = canvas.transform.Find("Screens");
        Transform resultScreen = screens.Find("Results Screen");
        Transform uiResults = resultsScreen.transform.Find("UI Results");
        Transform weaponAndPassiveItemChosen = uiResults.transform.Find("Weapon and Passive Item Chosen");
        Transform slotsWeapon = weaponAndPassiveItemChosen.transform.Find("Slots Weapon");

        foreach (Transform slot in slotsWeapon)
        {
            Image image = slot.GetComponent<Image>();
            if (image != null)
            {
                this.chosenWeaponsUI.Add(image);
            }
        }

        Debug.LogWarning(transform.name + ": LoadListChosenWeaponsUI", gameObject);
    }

    protected virtual void LoadListChosenPassiveItemsUI()
    {
        if(this.canvas == null) return;
        if (this.chosenPassiveItemsUI.Count > 0) return;

        Transform screens = canvas.transform.Find("Screens");
        Transform resultScreen = screens.Find("Results Screen");
        Transform uiResults = resultsScreen.transform.Find("UI Results");
        Transform weaponAndPassiveItemChosen = uiResults.transform.Find("Weapon and Passive Item Chosen");
        Transform slotsWeapon = weaponAndPassiveItemChosen.transform.Find("Slots Passive Item");

        foreach (Transform slot in slotsWeapon)
        {
            Image image = slot.GetComponent<Image>();
            if (image != null)
            {
                this.chosenPassiveItemsUI.Add(image);
            }
        }

        Debug.LogWarning(transform.name + ": LoadListChosenPassiveItemsUI", gameObject);
    }

    protected virtual void LoadTimeSurvivedDisplay()
    {
        if (this.canvas == null) return;
        if (this.timeSurvied != null) return;

        Transform screens = canvas.transform.Find("Screens");
        Transform resultsScreen = screens.transform.Find("Results Screen");
        GameObject uiResults = resultsScreen.transform.Find("UI Results").gameObject;
        Transform timeSurvivedHolder = uiResults.transform.Find("Time Survived Holder");
        this.timeSurvied = timeSurvivedHolder.transform.Find("Time Survived Display").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadTimeSurvivedDisplay", gameObject);
    }

    protected virtual void LoadTotalCoinDisplay()
    {
        if (this.canvas == null) return;
        if (this.totalCoin != null) return;

        Transform screens = canvas.transform.Find("Screens");
        Transform resultsScreen = screens.transform.Find("Results Screen");
        GameObject uiResults = resultsScreen.transform.Find("UI Results").gameObject;
        Transform coinCollectedHolder = uiResults.transform.Find("Coin Collected Holder");
        this.totalCoin = coinCollectedHolder.transform.Find("Coin Collected Display").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadTotalCoinDisplay", gameObject);
    }

    protected virtual void LoadTotalEnemiesDeadDisplay()
    {
        if (this.canvas == null) return;
        if (this.totalEnemiesDead != null) return;

        Transform screens = canvas.transform.Find("Screens");
        Transform resultsScreen = screens.transform.Find("Results Screen");
        GameObject uiResults = resultsScreen.transform.Find("UI Results").gameObject;
        Transform enemiesDeadHolder = uiResults.transform.Find("Enemies Dead Holder");
        this.totalEnemiesDead = enemiesDeadHolder.transform.Find("Enemies Dead Display").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadTotalEnemiesDeadDisplay", gameObject);
    }

    protected virtual void LoadStopwatchDisplay()
    {
        if(this.canvas == null) return;
        if (this.stopwatchDisplay != null) return;

        this.stopwatchDisplay = canvas.transform.Find("Stopwatch Display").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadStopwatchDisplay", gameObject);
    }
    
    #endregion

    protected override void Update()
    {
        this.ChangeScreen();
    }

    protected virtual void ChangeScreen()
    {
        switch(this.currentState)
        {
            case GameState.GamePlay:
                this.CheckForPauseAndResume();
                this.UpdateStopwatch();
                break;
            case GameState.Pause:
                this.CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    this.isGameOver = true;
                    Time.timeScale = 0f; //Stop the game entirely
                    this.DisplayGameOverScreen();
                }                
                break;
            case GameState.LevelUp:
                if (!choosingUpgrade)
                {
                    this.choosingUpgrade = true;
                    Time.timeScale = 0f; //Pause the game for now
                    this.levelUpScreen.SetActive(true);
                }
                break;
            case GameState.OpenTreasureChest:
                this.DisplayTreasureChestScreen();
                Time.timeScale = 0f;
                break;
            default:
                break;
        }
    }

    protected virtual void ChangeState(GameState newGameState)
    {
        this.currentState = newGameState;
    }

    public virtual void PauseGame()
    {
        if(this.currentState == GameState.Pause) return; 
        this.previousState = this.currentState;
        this.ChangeState(GameState.Pause);
        Time.timeScale = 0; //Stop the game
        this.pauseScreen.SetActive(true);
    }

    public virtual void ResumeGame()
    {
        if (this.currentState != GameState.Pause) return;
        this.ChangeState(this.previousState);
        Time.timeScale = 1; //Resume the game
        this.pauseScreen.SetActive(false);
    }

    protected virtual void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(this.currentState != GameState.Pause)
            {
                this.PauseGame();
            }
            else
            {
                this.ResumeGame();
            }
        }
    }

    protected virtual void DisableScreen()
    {
        this.pauseScreen.SetActive(false);
        this.resultsScreen.SetActive(false);
        this.levelUpScreen.SetActive(false);
        this.treasureChestScreen.SetActive(false);
        this.gameOverScreen.SetActive(false);
    }

    public virtual void GameOver()
    {
        this.ChangeState(GameState.GameOver);
        this.timeSurvied.text = this.stopwatchDisplay.text;
        this.totalCoin.text = this.coinText.text;
        this.totalEnemiesDead.text = this.enemiesDeadText.text;
        this.RecalculateTotalCoin();
        MySaveGame.Instance.SaveDataToLeaderboard(this.chosenCharacterName.text, this.timeSurvied.text, 
            this.levelReachedDisplay.text, this.totalCoin.text, this.totalEnemiesDead.text);
    }

    public virtual void DisplayResults()
    {
        this.resultsScreen.SetActive(true);
    }

    protected virtual void DisplayGameOverScreen()
    {
        this.gameOverScreen.SetActive(true);
    }


    public virtual void AssignCharacterDataUI(CharacterData characterSO)
    {
        this.chosenCharacterImage.sprite = characterSO.Icon;
        this.chosenCharacterName.text = characterSO.Name;
    }

    public virtual void AssignLevelReachedUI(int level)
    {
        this.levelReachedDisplay.text = level.ToString();
    }

    public virtual void AssignWeaponAndPassiveItemUI(List<PlayerInventory.Slot> chosenWeaponsData, List<PlayerInventory.Slot> chosenPassiveItemsData)
    {
        if(chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.LogError("Chosen weapon and passive item data list have different lenght");
            return;
        }

        //Assign chosen weapon data to chosenWeaponsUI
        for(int i = 0; i<chosenWeaponsUI.Count; i++)
        {
            //Check that the sprite of the corresponding element in chosenWeaponsData is not null
            if (chosenWeaponsData[i].image.sprite)
            {
                //Enable the corresonding element in the chosenWeaponsUI and set its sprite to the corresponding sprite in chosenWeaponsData
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].image.sprite;
            }
            else
            {
                //If the sprite is null, disable the corresponding in the chosenWeaponsUI
                chosenWeaponsUI[i].enabled = false;
            }
        }

        //Assign chosen passive item data to chosenPassiveItemsUI
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            //Check that the sprite of the corresponding element in chosenPassiveItemsData is not null
            if (chosenPassiveItemsData[i].image.sprite)
            {
                //Enable the corresonding element in the chosenPassiveItemsUI and set its sprite to the corresponding sprite in chosenPassiveItemsData
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].image.sprite;
            }
            else
            {
                //If the sprite is null, disable the corresponding in the chosenPassiveItemsUI
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }

    protected virtual void UpdateStopwatch()
    {
        this.stopwatchTime += Time.deltaTime;
        this.UpdateStopwatchDisplay();
    }

    protected virtual void UpdateStopwatchDisplay()
    {
        //Calculator the number mintues and seconds that have eslaped
        int minutes = Mathf.FloorToInt(this.stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(this.stopwatchTime % 60);

        //Update the stopwatchDisplay text to display the eslaped time
        this.stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public virtual void StartLevelUI()
    {
        this.ChangeState(GameState.LevelUp);
        this.playerInventory.SendMessage("RemoveAndApplyUpgrades");
    }

    public virtual void EndLevelUp()
    {
        this.choosingUpgrade = false;
        Time.timeScale = 1f; //Resume the game for now
        this.levelUpScreen.SetActive(false);
        this.ChangeState(GameState.GamePlay);
    }

    public void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (!instance.canvas) return;

        if(!instance.referenceCamera) instance.referenceCamera = Camera.main;

        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(text, target, duration, speed));
    }

    private IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f)
    {
        GameObject textObj = DamageFloatingTextSpawner.Instance.Spawn("DamageFloatingText", referenceCamera.WorldToScreenPoint(target.position), Quaternion.identity).gameObject;
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        TextMeshProUGUI textMesh = textObj.GetComponent<TextMeshProUGUI>();
        
        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the prefab.");
            yield break;
        }

        textMesh.text = text;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;
        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.fontSize = textFontsize;

        if (textFont) textMesh.font = textFont;
        rectTransform.position = referenceCamera.WorldToScreenPoint(target.position);
        textObj.transform.position = rectTransform.position;
        textObj.gameObject.SetActive(true);

        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;
        float yOffset = 0;
        Vector3 lastKnownPosition = target.position;
        while (t < duration)
        {
            // If the RectTransform is missing for whatever reason, end this loop.
            if (!rectTransform) break;

            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1 - t / duration);

            yOffset += speed * Time.deltaTime;
            rectTransform.position = referenceCamera.WorldToScreenPoint(lastKnownPosition + new Vector3(0, yOffset));

            // Wait for a frame and update the time.
            yield return w;
            t += Time.deltaTime;
        }

        if(t>= duration)
        {
            DamageFloatingTextSpawner.Instance.Despawn(textObj.transform);
        }
    }

    public virtual void RecalculateTotalCoin()
    {
        float totalCoin = PlayerPrefs.GetFloat("TotalCoin");
        PlayerPrefs.SetFloat("TotalCoin", totalCoin + this.coin);
        PlayerPrefs.Save();
    }

    public virtual void IncreaseCoin(float amount)
    {
        this.coin += amount;

        this.UpdateCoinText();
    }

    public virtual void IncreaseEnemiesDead()
    {
        this.enemiesDead ++;

        this.UpdateEnemiesDeadText();
    }

    protected virtual void UpdateCoinText()
    {
        this.coinText.text = coin.ToString();
    }

    protected virtual void UpdateEnemiesDeadText()
    {
        this.enemiesDeadText.text = this.enemiesDead.ToString();
    }

    protected virtual void DisplayTreasureChestScreen()
    {
        if (this.treasureChestScreen == null) return;

        this.treasureChestScreen.SetActive(true);
    }

    public virtual void OpenTreasureChest()
    {
        this.ChangeState(GameState.OpenTreasureChest);
    }

    public virtual void DisableTreasureChestScreen()
    {
        this.ChangeState(GameState.GamePlay);
        this.treasureChestScreen.SetActive(false);
        Time.timeScale = 1f;
        this.UpdateStopwatch();
    }
}
