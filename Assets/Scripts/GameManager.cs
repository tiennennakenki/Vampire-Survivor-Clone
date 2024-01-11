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
    [SerializeField] protected List<Image> chosenWeaponsUI = new List<Image>(6);
    [SerializeField] protected List<Image> chosenPassiveItemsUI = new List<Image>(6);
    //Flag to check if the game is over
    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 GameManager allow to exits");
        instance = this;
        this.DisableScreen();
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
    }

    public virtual void GameOver()
    {
        this.ChangeState(GameState.GameOver);
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
}
