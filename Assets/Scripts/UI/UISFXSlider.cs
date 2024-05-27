using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISFXSlider : SaiMonoBehaviour
{
    [Header("UI FX Slider")]
    private static UISFXSlider instance;
    public static UISFXSlider Instance => instance;
    public Slider fxSlider;
    [SerializeField] protected Image fill;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFXSlider();
        this.LoadFill();
    }

    protected virtual void LoadFXSlider()
    {
        if (this.fxSlider != null) return;

        this.fxSlider = GetComponent<Slider>();
        Debug.Log(transform.name + ": LoadFXSlider", gameObject);
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
        float value = Mathf.Round(this.fxSlider.value * 100) / 100;
        this.fill.fillAmount = value;

        SoundManager.Instance.FXVolume(value);
    }
}
