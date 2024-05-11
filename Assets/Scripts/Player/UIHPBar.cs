using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : SaiMonoBehaviour
{
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected Transform hpBarData;
    public Transform HpBarData => hpBarData;
    [SerializeField] protected Slider slider;
    public Slider Slider => slider;

    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    protected override void Awake()
    {
        base.Awake();
        this.playerStats = FindObjectOfType<PlayerStats>();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadHPBarData();
        this.LoadSlider();
        this.LoadPlayerStats();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponentInParent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadHPBarData()
    {
        if (this.hpBarData != null) return;
        this.hpBarData = this.playerCtrl.PlayerHPBar.transform;
        Debug.LogWarning(transform.name + ": LoadHPBarData", gameObject);
    }

    protected virtual void LoadSlider()
    {
        if (this.slider != null) return;
        this.slider = transform.GetComponent<Slider>();
        Debug.LogWarning(transform.name + ": LoadSlider", gameObject);
    }

    protected virtual void LoadPlayerStats()
    {
        if (this.playerStats != null) return;
        this.playerStats = this.playerCtrl.Model;
        Debug.LogWarning(transform.name + ": LoadPlayerStats", gameObject);
    }

    protected override void FixedUpdate()
    {
        this.UpdateHpBar();
    }

    public virtual void UpdateHpBar()
    {
        if (this.slider == null) return;
        IHPBarInterface hPBarInterface = this.hpBarData.GetComponent<IHPBarInterface>();
        if (hpBarData == null) return;
        //this.slider.value = hPBarInterface.HP();
        this.slider.value = this.playerStats.CurrentHealth / this.playerStats.characterData.stats.maxHealth;
    }
}
