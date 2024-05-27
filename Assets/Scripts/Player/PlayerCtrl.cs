using UnityEngine;

public class PlayerCtrl : SaiMonoBehaviour
{
    [Header("Player Ctrl")]
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;

    [SerializeField] protected PlayerStats model;
    public PlayerStats Model =>  model;

    [SerializeField] protected PlayerHP playerHPBar;
    public PlayerHP PlayerHPBar => playerHPBar;
    [SerializeField] protected Animation animationPlayer;
    public Animation AnimationPlayer => animationPlayer;
    [SerializeField] protected Canvas canvas;
    public Canvas Canvas => canvas;

    [SerializeField] protected PlayerCollector collector;
    public PlayerCollector Collector => collector;

    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 PlayerCtrl allow to exits");
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerHPBar();
        this.LoadCanvas();
        this.LoadAnimation();
        this.LoadModel();
        this.LoadCollector();
        this.LoadPlayerMovement();
        this.LoadCameraCanvas();
    }

    protected virtual void LoadCollector()
    {
        if (this.collector != null) return;
        this.collector = gameObject.GetComponentInChildren<PlayerCollector>();
        Debug.LogWarning(transform.name + ": LoadCollector", gameObject);
    }

    protected virtual void LoadPlayerHPBar()
    {
        if (this.playerHPBar != null) return;
        this.playerHPBar = gameObject.GetComponentInChildren<PlayerHP>();
        Debug.LogWarning(transform.name + ": LoadPlayerHPBar", gameObject);
    }

    protected virtual void LoadAnimation()
    {
        if (this.animationPlayer != null) return;
        this.animationPlayer = gameObject.GetComponentInChildren<Animation>();
        Debug.LogWarning(transform.name + ": LoadAnimation", gameObject);
    }

    protected virtual void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = gameObject.GetComponentInChildren<Canvas>();
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }

    protected virtual void LoadCameraCanvas()
    {
        if(this.canvas == null) return;

        this.canvas.renderMode = RenderMode.ScreenSpaceCamera;

        // Assuming you have a camera set somewhere else in your script or you need to get it
        Camera mainCamera = Camera.main; // Replace this with your actual camera if different

        if (mainCamera != null)
        {
            this.canvas.worldCamera = mainCamera;
        }
        else
        {
            Debug.LogWarning(transform.name + ": Main Camera not found", gameObject);
        }

        Debug.LogWarning(transform.name + ": LoadCameraCanvas", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = gameObject.GetComponentInChildren<PlayerStats>();
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }

    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = gameObject.GetComponentInChildren<PlayerMovement>();
        Debug.LogWarning(transform.name + ": LoadPlayerMovement", gameObject);
    }
}
