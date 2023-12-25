using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : SaiMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    public float horizontal;
    public float vertical;
    public bool isMoving; // Thêm biến để theo dõi trạng thái di chuyển

    protected override void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadAnimator();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.GetComponentInParent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadAnimator() {
        if(this.animator != null) return;
        this.animator = this.playerCtrl.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected override void Update()
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        // Set biến isMoving trong Animator để quyết định chạy animation di chuyển hoặc Idle
        animator.SetBool("IsMoving", isMoving);
    }
}
