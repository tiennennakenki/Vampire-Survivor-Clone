using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevel : SaiMonoBehaviour
{
    [SerializeField] protected List<Button> menuLevels = new List<Button>();
    public List<Button> MenuLevels => menuLevels;

    protected override void Awake()

    {
        base.Awake();
        this.LoadComponents();
        this.UnlockedLevel();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevel();
    }

    protected virtual void LoadLevel()
    {
        if (this.menuLevels.Count > 0) return;
        Transform levels = transform.Find("Stages");

        Button btnLevel;
        foreach (Transform level in levels)
        {
            btnLevel = level.GetComponentInChildren<Button>();
            this.menuLevels.Add(btnLevel);
        }
        Debug.LogWarning(transform.name + ": LoadLevel", gameObject);
    }

    protected virtual void UnlockedLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < menuLevels.Count; i++)
        {
            menuLevels[i].interactable = false;
            menuLevels[i].image.color = Color.black;
        }

        for (int j = 0; j < unlockedLevel; j++)
        {
            menuLevels[j].interactable = true;
            menuLevels[j].image.color = new Color(0.5960785f, 0.572549f, 0.2770203f, 0.7803922f);
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Stage " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
