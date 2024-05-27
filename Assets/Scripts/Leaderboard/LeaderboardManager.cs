using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : SaiMonoBehaviour
{
    [SerializeField] protected Canvas canvas;

    //Top1
    [SerializeField] protected Image avatarCharacterTop1;
    [SerializeField] protected TextMeshProUGUI characterNameTop1;
    [SerializeField] protected TextMeshProUGUI timeSurvivedTop1;
    [SerializeField] protected TextMeshProUGUI levelTextTop1;
    [SerializeField] protected TextMeshProUGUI totalCoinTextTop1;
    [SerializeField] protected TextMeshProUGUI totalEnemiesDeadTop1;

    //Top2
    [SerializeField] protected Image avatarCharacterTop2;
    [SerializeField] protected TextMeshProUGUI characterNameTop2;
    [SerializeField] protected TextMeshProUGUI timeSurvivedTop2;
    [SerializeField] protected TextMeshProUGUI levelTextTop2;
    [SerializeField] protected TextMeshProUGUI totalCoinTextTop2;
    [SerializeField] protected TextMeshProUGUI totalEnemiesDeadTop2;

    //Top3
    [SerializeField] protected Image avatarCharacterTop3;
    [SerializeField] protected TextMeshProUGUI characterNameTop3;
    [SerializeField] protected TextMeshProUGUI timeSurvivedTop3;
    [SerializeField] protected TextMeshProUGUI levelTextTop3;
    [SerializeField] protected TextMeshProUGUI totalCoinTextTop3;
    [SerializeField] protected TextMeshProUGUI totalEnemiesDeadTop3;
    protected override void Awake()
    {
        base.Awake();
        this.GetLeaderboard();
    }

    #region LoadComponents 
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadTop1();
        this.LoadTop2();
        this.LoadTop3();
    }

    protected virtual void LoadTop1()
    {
        this.LoadAvatarCharacterTop1();
        this.LoadCharacterNameTop1();
        this.LoadTimeSurvivedTop1();
        this.LoadLevelTextTop1();
        this.LoadTotalCoinTextTop1();
        this.LoadTotalEnemiesDeadTextTop1();
    }

    protected virtual void LoadTop2()
    {
        this.LoadAvatarCharacterTop2();
        this.LoadCharacterNameTop2();
        this.LoadTimeSurvivedTop2();
        this.LoadLevelTextTop2();
        this.LoadTotalCoinTextTop2();
        this.LoadTotalEnemiesDeadTextTop2();
    }

    protected virtual void LoadTop3()
    {
        this.LoadAvatarCharacterTop3();
        this.LoadCharacterNameTop3();
        this.LoadTimeSurvivedTop3();
        this.LoadLevelTextTop3();
        this.LoadTotalCoinTextTop3();
        this.LoadTotalEnemiesDeadTextTop3();
    }

    protected virtual void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = FindObjectOfType<Canvas>();

        Debug.Log(transform.name + ":LoadCanvas" + gameObject);
    }

    protected virtual void LoadAvatarCharacterTop1()
    {
        if(this.canvas == null) return;
        if(this.avatarCharacterTop1 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top1 = leaderboard.Find("Top 1");
        Transform avatarcharacter = top1.Find("Avatar Character");
        this.avatarCharacterTop1 = avatarcharacter.Find("Avatar").GetComponent<Image>();

        Debug.Log(transform.name + ":LoadAvatarCharacterTop1" + gameObject);
    }

    protected virtual void LoadAvatarCharacterTop2()
    {
        if (this.canvas == null) return;
        if (this.avatarCharacterTop2 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top2 = leaderboard.Find("Top 2");
        Transform avatarcharacter = top2.Find("Avatar Character");
        this.avatarCharacterTop2 = avatarcharacter.Find("Avatar").GetComponent<Image>();

        Debug.Log(transform.name + ":LoadAvatarCharacterTop2" + gameObject);
    }

    protected virtual void LoadAvatarCharacterTop3()
    {
        if (this.canvas == null) return;
        if (this.avatarCharacterTop3 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top3 = leaderboard.Find("Top 3");
        Transform avatarcharacter = top3.Find("Avatar Character");
        this.avatarCharacterTop3 = avatarcharacter.Find("Avatar").GetComponent<Image>();

        Debug.Log(transform.name + ":LoadAvatarCharacterTop3" + gameObject);
    }

    protected virtual void LoadCharacterNameTop1()
    {
        if (this.canvas == null) return;
        if (this.characterNameTop1 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top1 = leaderboard.Find("Top 1");
        this.characterNameTop1 = top1.Find("Name Character").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadCharacterNameTop1" + gameObject);
    }

    protected virtual void LoadCharacterNameTop2()
    {
        if (this.canvas == null) return;
        if (this.characterNameTop2 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top2 = leaderboard.Find("Top 2");
        this.characterNameTop2 = top2.Find("Name Character").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadCharacterNameTop2" + gameObject);
    }

    protected virtual void LoadCharacterNameTop3()
    {
        if (this.canvas == null) return;
        if (this.characterNameTop3 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top3 = leaderboard.Find("Top 3");
        this.characterNameTop3 = top3.Find("Name Character").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadCharacterNameTop3" + gameObject);
    }
    protected virtual void LoadTimeSurvivedTop1()
    {
        if (this.canvas == null) return;
        if (this.timeSurvivedTop1 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top1 = leaderboard.Find("Top 1");
        this.timeSurvivedTop1 = top1.Find("Time Survived").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTimeSurvivedTop1" + gameObject);
    }

    protected virtual void LoadTimeSurvivedTop2()
    {
        if (this.canvas == null) return;
        if (this.timeSurvivedTop2 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top2 = leaderboard.Find("Top 2");
        this.timeSurvivedTop2 = top2.Find("Time Survived").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTimeSurvivedTop2" + gameObject);
    }

    protected virtual void LoadTimeSurvivedTop3()
    {
        if (this.canvas == null) return;
        if (this.timeSurvivedTop3 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top3 = leaderboard.Find("Top 3");
        this.timeSurvivedTop3 = top3.Find("Time Survived").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTimeSurvivedTop3" + gameObject);
    }

    protected virtual void LoadLevelTextTop1()
    {
        if (this.canvas == null) return;
        if (this.levelTextTop1 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top1 = leaderboard.Find("Top 1");
        this.levelTextTop1 = top1.Find("Level Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadLevelTextTop1" + gameObject);
    }

    protected virtual void LoadLevelTextTop2()
    {
        if (this.canvas == null) return;
        if (this.levelTextTop2 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top2 = leaderboard.Find("Top 2");
        this.levelTextTop2 = top2.Find("Level Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadLevelTextTop2" + gameObject);
    }

    protected virtual void LoadLevelTextTop3()
    {
        if (this.canvas == null) return;
        if (this.levelTextTop3 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top3 = leaderboard.Find("Top 3");
        this.levelTextTop3 = top3.Find("Level Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadLevelTextTop3" + gameObject);
    }

    protected virtual void LoadTotalCoinTextTop1()
    {
        if (this.canvas == null) return;
        if (this.totalCoinTextTop1 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top1 = leaderboard.Find("Top 1");
        this.totalCoinTextTop1 = top1.Find("Total Coin Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTotalCoinTextTop1" + gameObject);
    }

    protected virtual void LoadTotalCoinTextTop2()
    {
        if (this.canvas == null) return;
        if (this.totalCoinTextTop2 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top2 = leaderboard.Find("Top 2");
        this.totalCoinTextTop2 = top2.Find("Total Coin Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTotalCoinTextTop2" + gameObject);
    }

    protected virtual void LoadTotalCoinTextTop3()
    {
        if (this.canvas == null) return;
        if (this.totalCoinTextTop3 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top3 = leaderboard.Find("Top 3");
        this.totalCoinTextTop3 = top3.Find("Total Coin Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTotalCoinTextTop3" + gameObject);
    }

    protected virtual void LoadTotalEnemiesDeadTextTop1()
    {
        if (this.canvas == null) return;
        if (this.totalEnemiesDeadTop1 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top1 = leaderboard.Find("Top 1");
        this.totalEnemiesDeadTop1 = top1.Find("Total Enemies Dead Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTotalEnemiesDeadTextTop1" + gameObject);
    }

    protected virtual void LoadTotalEnemiesDeadTextTop2()
    {
        if (this.canvas == null) return;
        if (this.totalEnemiesDeadTop2 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top2 = leaderboard.Find("Top 2");
        this.totalEnemiesDeadTop2 = top2.Find("Total Enemies Dead Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTotalEnemiesDeadTextTop2" + gameObject);
    }

    protected virtual void LoadTotalEnemiesDeadTextTop3()
    {
        if (this.canvas == null) return;
        if (this.totalEnemiesDeadTop3 != null) return;

        Transform screen = this.canvas.transform.Find("Screen");
        Transform leaderboardScreen = screen.Find("Leaderboard Screen");
        Transform leaderboard = leaderboardScreen.Find("Leaderboard");
        Transform top3 = leaderboard.Find("Top 1");
        this.totalEnemiesDeadTop3 = top3.Find("Total Enemies Dead Text").GetComponent<TextMeshProUGUI>();

        Debug.Log(transform.name + ":LoadTotalEnemiesDeadTextTop3" + gameObject);
    }
    #endregion

    protected virtual void SetAvatarCharacterOnTop(string characterName, Image avatarCharacterTop)
    {
        if (characterName == "-")
        {
            avatarCharacterTop.gameObject.SetActive(false);
            Debug.Log("Character name = " + characterName);
            return;
        }
        string path = "Characters/" + characterName;
        CharacterData characterData = Resources.Load<CharacterData>(path);

        avatarCharacterTop.sprite = characterData.Icon;
        avatarCharacterTop.gameObject.SetActive(true);
    }

    protected virtual void GetLeaderboard()
    {
        // Filter and arrange the list leaderboard by timeSurvived in descending order
        var sortedLeaderboard = MySaveGame.Instance.leaderboard.listTop
            .Where(top => TimeSpan.TryParse(top.timeSurvived, out _)) // Chỉ lấy các mục có timeSurvived hợp lệ
            .OrderByDescending(top => TimeSpan.Parse(top.timeSurvived))
            .ToList();

        // Update value for the top
        if (sortedLeaderboard.Count >= 1)
        {
            var top1 = sortedLeaderboard[0];
            this.characterNameTop1.text = top1.nameCharacter;
            this.timeSurvivedTop1.text = top1.timeSurvived;
            this.levelTextTop1.text = top1.level;
            this.totalCoinTextTop1.text = top1.totalCoin;
            this.totalEnemiesDeadTop1.text = top1.totalEnemiesDead;
        }

        if (sortedLeaderboard.Count >= 2)
        {
            var top2 = sortedLeaderboard[1];
            this.characterNameTop2.text = top2.nameCharacter;
            this.timeSurvivedTop2.text = top2.timeSurvived;
            this.levelTextTop2.text = top2.level;
            this.totalCoinTextTop2.text = top2.totalCoin;
            this.totalEnemiesDeadTop2.text = top2.totalEnemiesDead;
        }

        if (sortedLeaderboard.Count >= 3)
        {
            var top3 = sortedLeaderboard[2];
            this.characterNameTop3.text = top3.nameCharacter;
            this.timeSurvivedTop3.text = top3.timeSurvived;
            this.levelTextTop3.text = top3.level;
            this.totalCoinTextTop3.text = top3.totalCoin;
            this.totalEnemiesDeadTop3.text = top3.totalEnemiesDead;
        }

        this.SetAvatarCharacterOnTop(this.characterNameTop1.text, this.avatarCharacterTop1);
        this.SetAvatarCharacterOnTop(this.characterNameTop2.text, this.avatarCharacterTop2);
        this.SetAvatarCharacterOnTop(this.characterNameTop3.text, this.avatarCharacterTop3);
    }
    

}
