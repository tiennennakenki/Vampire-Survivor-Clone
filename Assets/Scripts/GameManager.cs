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

    [SerializeField] protected GameState currentState;
    [SerializeField] protected GameState previousState;

    //Screens
    [Header("Screens")]
    [SerializeField] protected GameObject pauseScreen;
    [SerializeField] protected GameObject resultsScreen;
    [SerializeField] protected GameObject levelUpScreen;

    //Current Stat Display
    [Header("Current Stat Displays")]
    public TextMeshProUGUI currentHealthDisplay;
    public TextMeshProUGUI currentRecoveryDisplay;
    public TextMeshProUGUI currentMoveSpeedDisplay;
    public TextMeshProUGUI currentMightDisplay;
    public TextMeshProUGUI currentProjectilesSpeedDisplay;
    public TextMeshProUGUI currentMagnetDisplay;

    //Current Stat Display
    [Header("Results Screen Displays")]
    [SerializeField] protected Image chosenCharacterImage;
    [SerializeField] protected TextMeshProUGUI chosenCharacterName;
    [SerializeField] protected TextMeshProUGUI levelReachedDisplay;
    [SerializeField] protected TextMeshProUGUI timeSurvied;
    [SerializeField] protected List<Image> chosenWeaponsUI = new List<Image>(6);
    [SerializeField] protected List<Image> chosenPassiveItemsUI = new List<Image>(6);

    [Header("Damage Text Setting")]
    public Canvas damageTextCanvas;
    public float textFontsize = 20f;
    public TMP_FontAsset textFont;
    public Camera referenceCamera;

    //Flag to check if the game is over
    public bool isGameOver = false;

    //Flag to check if the player is choosing their upgrades
    public bool choosingUpgrade;

    //Reference to the player's game object
    public InventoryManager playerObj;

    //Stopwatch UI
    [Header("Stopwatch")]
    [SerializeField] protected float timeLimit = 15f; //The time limit in seconds
    [SerializeField] protected float stopwatchTime; //The current time eslaped since the stopwatch started
    [SerializeField] protected TextMeshProUGUI stopwatchDisplay;


    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 GameManager allow to exits");
        instance = this;
        this.DisableScreen();
        this.playerObj = FindObjectOfType<InventoryManager>();
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPauseScreen();
        this.LoadResultsScreen();
        this.LoadCurrentHealthDisplay();
        this.LoadCurrentRecoveryDisplay();
        this.LoadCurrentMoveSpeedDisplay();
        this.LoadCurrentMightDisplay();
        this.LoadCurrentProjectilesSpeedDisplay();
        this.LoadCurrentMagnetDisplay();
        this.LoadChosenCharacterImage();
        this.LoadChosenCharacterName();
        this.LoadLevelReachedDisplay();
        this.LoadListChosenWeaponsUI();
        this.LoadListChosenPassiveItemsUI();
        this.LoadTimeSurvivedDisplay();
        this.LoadStopwatchDisplay();
        this.LoadLevelUpScreen();
    }

    protected virtual void LoadPauseScreen()
    {
        if (this.pauseScreen != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        Transform screens = canvas.transform.Find("Screens");
        this.pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        Debug.LogWarning(transform.name + ": LoadPauseScreen", gameObject);
    }

    protected virtual void LoadResultsScreen()
    {
        if (this.resultsScreen != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        Transform screens = canvas.transform.Find("Screens");
        this.resultsScreen = screens.transform.Find("Results Screen").gameObject;
        Debug.LogWarning(transform.name + ": LoadPauseScreen", gameObject);
    }

    protected virtual void LoadCurrentHealthDisplay()
    {
        if(this.currentHealthDisplay != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        GameObject currentStatsDisplay = pauseScreen.transform.Find("Current Stats Display").gameObject;
        this.currentHealthDisplay = currentStatsDisplay.transform.Find("Current Health Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCurrentHealthDisplay", gameObject);
    }

    protected virtual void LoadCurrentRecoveryDisplay()
    {
        if (this.currentRecoveryDisplay != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        GameObject currentStatsDisplay = pauseScreen.transform.Find("Current Stats Display").gameObject;
        this.currentRecoveryDisplay = currentStatsDisplay.transform.Find("Current Recovery Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCurrentRecoveryDisplay", gameObject);
    }

    protected virtual void LoadCurrentMightDisplay()
    {
        if (this.currentMightDisplay != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        GameObject currentStatsDisplay = pauseScreen.transform.Find("Current Stats Display").gameObject;
        this.currentMightDisplay = currentStatsDisplay.transform.Find("Current Might Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCurrentMightDisplay", gameObject);
    }

    protected virtual void LoadCurrentMoveSpeedDisplay()
    {
        if (this.currentMoveSpeedDisplay != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        GameObject currentStatsDisplay = pauseScreen.transform.Find("Current Stats Display").gameObject;
        this.currentMoveSpeedDisplay = currentStatsDisplay.transform.Find("Current Move Speed Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCurrentMoveSpeedDisplay", gameObject);
    }

    protected virtual void LoadCurrentProjectilesSpeedDisplay()
    {
        if (this.currentProjectilesSpeedDisplay != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        GameObject currentStatsDisplay = pauseScreen.transform.Find("Current Stats Display").gameObject;
        this.currentProjectilesSpeedDisplay = currentStatsDisplay.transform.Find("Current Projectiles Speed Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCurrentProjectilesDisplay", gameObject);
    }

    protected virtual void LoadCurrentMagnetDisplay()
    {
        if (this.currentMagnetDisplay != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject pauseScreen = screens.transform.Find("Pause Screen").gameObject;
        GameObject currentStatsDisplay = pauseScreen.transform.Find("Current Stats Display").gameObject;
        this.currentMagnetDisplay = currentStatsDisplay.transform.Find("Current Magnet Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCurrentMagnetDisplay", gameObject);
    }

    protected virtual void LoadChosenCharacterImage()
    {
        if (this.chosenCharacterImage != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject resultsScreen = screens.transform.Find("Results Screen").gameObject;
        GameObject chosenCharacterHolder = resultsScreen.transform.Find("Chosen Character Holder").gameObject;
        this.chosenCharacterImage = chosenCharacterHolder.transform.Find("Chosen Character Image").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadChosenCharacterImage", gameObject);
    }

    protected virtual void LoadChosenCharacterName()
    {
        if (this.chosenCharacterName != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject resultsScreen = screens.transform.Find("Results Screen").gameObject;
        GameObject chosenCharacterHolder = resultsScreen.transform.Find("Chosen Character Holder").gameObject;
        this.chosenCharacterName = chosenCharacterHolder.transform.Find("Chosen Character Name").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadChosenCharacterName", gameObject);
    }

    protected virtual void LoadLevelReachedDisplay()
    {
        if (this.levelReachedDisplay != null) return;
        Canvas canvas = FindObjectOfType<Canvas>();
        GameObject screens = canvas.transform.Find("Screens").gameObject;
        GameObject resultsScreen = screens.transform.Find("Results Screen").gameObject;
        GameObject levelReachedHolder = resultsScreen.transform.Find("Level Reached Holder").gameObject;
        this.levelReachedDisplay = levelReachedHolder.transform.Find("Level Reached Display").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadLevelReachedDisplay", gameObject);
    }

    protected virtual void LoadListChosenWeaponsUI()
    {
        if (this.chosenWeaponsUI.Count > 0) return;

        Canvas canvas = FindObjectOfType<Canvas>();
        Transform screens = canvas.transform.Find("Screens");
        Transform weaponAndPassiveItemChosen = screens.transform.Find("Weapon and Passive Item Chosen");
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
        if (this.chosenPassiveItemsUI.Count > 0) return;

        Canvas canvas = FindObjectOfType<Canvas>();
        Transform screens = canvas.transform.Find("Screens");
        Transform weaponAndPassiveItemChosen = screens.transform.Find("Weapon and Passive Item Chosen");
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
        if (this.timeSurvied != null) return;

        Canvas canvas = FindObjectOfType<Canvas>();
        Transform screens = canvas.transform.Find("Screens");
        Transform resultsScreen = screens.transform.Find("Results Screen");
        Transform timeSurvivedHolder = resultsScreen.transform.Find("Time Survived Holder");
        this.timeSurvied = timeSurvivedHolder.transform.Find("Time Survived Display").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadTimeSurvivedDisplay", gameObject);
    }

    protected virtual void LoadStopwatchDisplay()
    {
        if (this.stopwatchDisplay != null) return;

        Canvas canvas = FindObjectOfType<Canvas>();
        this.stopwatchDisplay = canvas.transform.Find("Stopwatch Display").GetComponent<TextMeshProUGUI>();

        Debug.LogWarning(transform.name + ": LoadStopwatchDisplay", gameObject);
    }
    protected virtual void LoadLevelUpScreen()
    {
        if (this.levelUpScreen != null) return;

        Canvas canvas = FindObjectOfType<Canvas>();
        Transform screens = canvas.transform.Find("Screens");
        this.levelUpScreen = screens.transform.Find("Level Up Screen").gameObject;

        Debug.LogWarning(transform.name + ": LoadLevelUpScreen", gameObject);
    }
    #endregion

    protected override void Update()
    {
        base.Update();
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
                    Debug.Log("Game is over");
                    this.DisplayResults();
                }                
                break;
            case GameState.LevelUp:
                if (!choosingUpgrade)
                {
                    this.choosingUpgrade = true;
                    Time.timeScale = 0f; //Pause the game for now
                    Debug.Log("Upgrade shown");
                    this.levelUpScreen.SetActive(true);
                }
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
        Debug.Log("Game is paused");
    }

    public virtual void ResumeGame()
    {
        if (this.currentState != GameState.Pause) return;
        this.ChangeState(this.previousState);
        Time.timeScale = 1; //Resume the game
        this.pauseScreen.SetActive(false);
        Debug.Log("Game is resumed");
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
    }

    public virtual void GameOver()
    {
        this.ChangeState(GameState.GameOver);
        this.timeSurvied.text = this.stopwatchDisplay.text;
    }

    protected virtual void DisplayResults()
    {
        this.resultsScreen.SetActive(true);
    }

    public virtual void AssignCharacterDataUI(CharacterSO characterSO)
    {
        this.chosenCharacterImage.sprite = characterSO.Icon;
        this.chosenCharacterName.text = characterSO.Name;
    }

    public virtual void AssignLevelReachedUI(int level)
    {
        this.levelReachedDisplay.text = level.ToString();
    }

    public virtual void AssignWeaponAndPassiveItemUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
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
            if (chosenWeaponsData[i].sprite)
            {
                //Enable the corresonding element in the chosenWeaponsUI and set its sprite to the corresponding sprite in chosenWeaponsData
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
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
            if (chosenPassiveItemsData[i].sprite)
            {
                //Enable the corresonding element in the chosenPassiveItemsUI and set its sprite to the corresponding sprite in chosenPassiveItemsData
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
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

        if(this.stopwatchTime >= this.timeLimit)
        {
            this.playerObj.player.SendMessage("Kill");
        }
    }

    protected virtual void UpdateStopwatchDisplay()
    {
        //Calculator the number mintues and seconds that have eslaped
        int minutes = Mathf.FloorToInt(this.stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(this.stopwatchTime % 60);

        //Update the stopwatchDisplay text to display the eslaped time
        this.stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public virtual void StartLevelUP()
    {
        this.ChangeState(GameState.LevelUp);
        this.playerObj.SendMessage("RemoveAndApplyUpgrades");
    }

    public virtual void EndLevelUP()
    {
        this.choosingUpgrade = false;
        Time.timeScale = 1f; //Resume the game for now
        this.levelUpScreen.SetActive(false);
        this.ChangeState(GameState.GamePlay);
    }

    public void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (!instance.damageTextCanvas) return;

        if(!instance.referenceCamera) instance.referenceCamera = Camera.main;

        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(text, target, duration, speed));
    }

    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f)
    {
        GameObject textObj = new GameObject("Damage Floating Text");
        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI textMesh = textObj.AddComponent<TextMeshProUGUI>();
        textMesh.text = text;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;
        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.fontSize = textFontsize;

        if (textFont) textMesh.font = textFont;
        rectTransform.position = referenceCamera.WorldToScreenPoint(target.position);

        Destroy(textObj, duration);

        textObj.transform.SetParent(instance.damageTextCanvas.transform);

        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;
        float yOffset = 0;
        while (t < duration)
        {
            yield return w;
            t += Time.deltaTime;

            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1 - t / duration);

            yOffset += speed * Time.deltaTime;
            //rectTransform.position = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0,yOffset));
        }
    }
}
