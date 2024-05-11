using UnityEngine;

public class PlayerMovement : SaiMonoBehaviour
{
    //public const float DEFAULT_MOVESPEED = 2f;

    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;
    [SerializeField] public Vector3 direction;

    [SerializeField] protected Animation animate;
    [SerializeField] protected bool isMoving = false; // Biến để theo dõi trạng thái di chuyển
    [SerializeField] protected float previousHorizontal = 0f;
    [SerializeField] protected float previousVertical = 0f;
    public Vector2 lastMovedVector;
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected float moveSpeed;


    //Reference
    [SerializeField] protected Rigidbody2D rpg2d;
    public Rigidbody2D Rpg2d => rpg2d;
    public PlayerStats playerStats;
    protected override void Awake()
    {
        base.Awake();
        direction = new Vector3();
        this.lastMovedVector = new Vector2(1, 0f); //If we start the game and player don't move then the lastMovedVector default is right
    }

    protected override void Start()
    {
        moveSpeed = playerCtrl.Model.characterData.stats.moveSpeed;
    }


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadRigidbody2D();
        this.LoadAnimate();
        this.LoadPlayerStats();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.GetComponentInParent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rpg2d != null) return;
        this.rpg2d = this.playerCtrl.Model.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadAnimate()
    {
        if (this.animate != null) return;
        this.animate = this.playerCtrl.AnimationPlayer;
        Debug.LogWarning(transform.name + ": LoadAnimate", gameObject);
    }

    protected virtual void LoadPlayerStats()
    {
        if (this.playerStats != null) return;
        this.playerStats = this.playerCtrl.Model;
        Debug.LogWarning(transform.name + ": LoadPlayerStats", gameObject);
    }

    protected override void Update()
    {
        this.Moving();
    }

    protected virtual void Moving()
    {
        if (GameManager.Instance.isGameOver) return;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        // Kiểm tra nếu người chơi đang di chuyển
        if (direction.x != 0 || direction.y != 0)
        {
            // Kiểm tra xem hướng mới có khác với hướng trước đó hay không
            if (direction.x != previousHorizontal || direction.y != previousVertical)
            {
                if(isFacingRight && direction.x < 0 || !isFacingRight && direction.x >0)
                {
                    isFacingRight = !isFacingRight;

                    Vector3 scale = playerCtrl.AnimationPlayer.transform.localScale;
                    scale.x = playerCtrl.AnimationPlayer.transform.localScale.x * -1;
                    playerCtrl.AnimationPlayer.transform.localScale = scale;
                }
                // Chuyển đổi animation tại đây
                animate.horizontal = Mathf.Abs(direction.x);
                animate.vertical = Mathf.Abs(direction.y);
                lastMovedVector = new Vector2(direction.x, direction.y);
            }

            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Lưu trữ hướng hiện tại để so sánh ở frame tiếp theo
        previousHorizontal = direction.x;
        previousVertical = direction.y;

        transform.parent.Translate(direction * this.moveSpeed * Time.deltaTime);
        //lastMovedVector = new Vector2(previousHorizontal, previousVertical);

        //direction *= moveSpeed;
        //rpg2d.velocity = direction;
        

        // Set giá trị của biến isMoving trong script Animate
        animate.isMoving = isMoving;
    }
}
