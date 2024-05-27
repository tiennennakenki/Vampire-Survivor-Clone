using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MySaveGame : SaiMonoBehaviour
{
    [Header("My Save Game")]
    private static MySaveGame instance;
    public static MySaveGame Instance => instance;

    public Leaderboard leaderboard;

    public bool musicStatus;
    public bool fxStatus;
    public float musicVolume = 1;
    public float fxVolume = 1;


    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    protected override void Start()
    {
        base.Start();
        this.Loading();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)){

            this.SaveLeaderboard();
        }
    }

    protected virtual void Loading()
    {
        this.musicStatus = intToBool(PlayerPrefs.GetInt("setting_music_status"));
        SoundManager.Instance.MusicStatus(this.musicStatus);

        this.fxStatus = intToBool(PlayerPrefs.GetInt("setting_fx_status"));
        SoundManager.Instance.FXStatus(this.fxStatus);

        this.musicVolume = PlayerPrefs.GetFloat("setting_music_volume");
        SoundManager.Instance.MusicVolume(this.musicVolume);

        this.fxVolume = PlayerPrefs.GetFloat("setting_fx_volume");
        SoundManager.Instance.FXVolume(this.fxVolume);

        this.LoadLeaderboard();
    }

    public virtual void SettingMusicStatus(bool status)
    {
        this.musicStatus = status;
        PlayerPrefs.SetInt("setting_music_status", boolToInt(this.musicStatus));
    }

    public virtual void SettingFXStatus(bool status)
    {
        this.fxStatus = status;
        PlayerPrefs.SetInt("setting_fx_status", boolToInt(this.fxStatus));
    }

    public virtual void SettingMusicVolume(float volume)
    {
        this.musicVolume = volume;
        PlayerPrefs.SetFloat("setting_music_volume", musicVolume);
    }

    public virtual void SettingFXVolume(float volume)
    {
        this.fxVolume = volume;
        PlayerPrefs.SetFloat("setting_fx_volume", fxVolume);
    }

    protected virtual int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    protected virtual bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

    public virtual void LoadLeaderboard()
    {
        string file = "leaderboard.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        if(!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }

        this.leaderboard = JsonUtility.FromJson<Leaderboard>(File.ReadAllText(filePath));
        Debug.Log("Load done!");
    }

    public virtual void SaveLeaderboard()
    { 
        string file = "leaderboard.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        string json = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(filePath, json);
        Debug.Log("File saved, at path " + filePath);
    }

    public virtual void SaveDataToLeaderboard(string nameCharacter, string timeSurvived, string level, string totalCoin, string totalEnemiesDead)
    {
        if (this.leaderboard.listTop == null)
        {
            this.leaderboard.listTop = new List<TopOfLeaderboard>();
        }

        // Create new obj for TopOfLeaderboard
        TopOfLeaderboard newTop = new TopOfLeaderboard
        {
            nameCharacter = nameCharacter,
            timeSurvived = timeSurvived,
            level = level,
            totalCoin = totalCoin,
            totalEnemiesDead = totalEnemiesDead
        };

        this.leaderboard.listTop.Add(newTop);

        // Reorder the listTop by timeSurvived in descending order
        this.leaderboard.listTop = this.leaderboard.listTop
            .Where(top => TimeSpan.TryParse(top.timeSurvived, out _)) // Filter entries with a valid timeSurvived
            .OrderByDescending(top => TimeSpan.Parse(top.timeSurvived))
            .ToList();
        //
        this.RemoveDataLeaderboard(this.leaderboard.listTop);

        // Save list of Leaderboard
        this.SaveLeaderboard();
    }

    protected virtual void RemoveDataLeaderboard(List<TopOfLeaderboard> listTop)
    {
        for(int i = 3; i < listTop.Count; i++)
        {
            listTop.Remove(listTop[i]);
        }
    }
}
