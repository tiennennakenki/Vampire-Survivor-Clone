using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionScreen : SaiMonoBehaviour
{
    [SerializeField] protected Toggle musicToggle;
    [SerializeField] protected Toggle fxToggle;

    protected override void Awake()
    {
        base.Awake();
        this.LoadSettingFromSave();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMusicToggle();
        this.LoadFXToggle();
    }

    protected virtual void LoadMusicToggle()
    {
        if (musicToggle != null) return;

        Transform music = transform.Find("Music");
        this.musicToggle = music.Find("Music Toggle").GetComponent<Toggle>();
    }

    protected virtual void LoadFXToggle()
    {
        if (this.fxToggle != null) return;

        Transform sfx = transform.Find("SFX");
        this.fxToggle = sfx.Find("SFX Toggle").GetComponent<Toggle>();
    }

    public virtual void UpdateMusic()
    {
        SoundManager.Instance.MusicStatus(this.musicToggle.isOn);
    }

    public virtual void UpdateFX()
    {
        SoundManager.Instance.FXStatus(this.fxToggle.isOn);
    }

    public virtual void LoadSettingFromSave()
    {
        this.musicToggle.isOn = MySaveGame.Instance.musicStatus;
        this.fxToggle.isOn = MySaveGame.Instance.fxStatus;

        UIMusicSlider.Instance.musicSlider.value = MySaveGame.Instance.musicVolume;
        UISFXSlider.Instance.fxSlider.value = MySaveGame.Instance.fxVolume;
    }
}
