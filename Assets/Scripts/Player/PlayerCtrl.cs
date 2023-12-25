using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerCtrl : SaiMonoBehaviour
{
    [SerializeField] protected GameObject model;
    public GameObject Model =>  model;

    [SerializeField] protected PlayerHP playerHPBar;
    public PlayerHP PlayerHPBar => playerHPBar;
    [SerializeField] protected Animation animation;
    public Animation Animation => animation;
    [SerializeField] protected Canvas canvas;
    public Canvas Canvas => canvas;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerHPBar();
        this.LoadCanvas();
        this.LoadAnimation();
        this.LoadModel();
    }

    protected virtual void LoadPlayerHPBar()
    {
        if (this.playerHPBar != null) return;
        //this.playerHPBar = transform.Find("PlayerHPBar").gameObject;
        this.playerHPBar = gameObject.GetComponentInChildren<PlayerHP>();
        Debug.LogWarning(transform.name + ": LoadPlayerHPBar", gameObject);
    }

    protected virtual void LoadAnimation()
    {
        if (this.animation != null) return;
        //this.animation = transform.Find("Animation").gameObject;
        this.animation = gameObject.GetComponentInChildren<Animation>();
        Debug.LogWarning(transform.name + ": LoadAnimation", gameObject);
    }

    protected virtual void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = gameObject.GetComponentInChildren<Canvas>();
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").gameObject;
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }
}
