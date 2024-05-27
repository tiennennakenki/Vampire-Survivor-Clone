using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMusicSlider : SaiMonoBehaviour
{
    [Header("UI Music Slider")]
    private static UIMusicSlider instance;
    public static UIMusicSlider Instance => instance;

    public Slider musicSlider;
    [SerializeField] protected Image fill;

    protected override void Awake()
    {
        base.Awake();
        if(instance == null)
        {
            instance = this;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMusicSlider();
        this.LoadFill();
    }

    protected virtual void LoadMusicSlider()
    {
        if (this.musicSlider != null) return;

        this.musicSlider = GetComponent<Slider>();
        Debug.Log(transform.name + ": LoadMusicSlider", gameObject);
    }

    protected virtual void LoadFill()
    {
        if (this.fill != null) return;

        Transform fillArea = transform.Find("Fill Area");
        this.fill = fillArea.transform.Find("Fill").GetComponent<Image>();
        Debug.Log(transform.name + ": LoadFill", gameObject);
    }

    public virtual void SlideUpdating()
    {
        float value = Mathf.Round(this.musicSlider.value * 100) / 100;
        this.fill.fillAmount = value;

        SoundManager.Instance.MusicVolume(value);
    }
}
