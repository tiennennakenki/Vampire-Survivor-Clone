using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : SaiMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;
    [SerializeField] protected Rigidbody2D rpg2d;
    public Rigidbody2D Rpg2d => rpg2d;
    [SerializeField] public Vector3 direction;

    [SerializeField] float moveSpeed = 3f;
    public float MoveSpeed => moveSpeed;

    [SerializeField] protected Animation animate;
    [SerializeField] protected bool isMoving = false; // Biến để theo dõi trạng thái di chuyển
    [SerializeField] protected float previousHorizontal = 0f;
    [SerializeField] protected float previousVertical = 0f;

    protected override void Awake()
    {
        base.Awake();
        direction = new Vector3();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadRigidbody2D();
        this.LoadAnimate();
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
        this.animate = this.playerCtrl.Animation;
        Debug.LogWarning(transform.name + ": LoadAnimate", gameObject);
    }

    protected override void Update()
    {
        base.Update();
        this.Moving();
    }

    protected virtual void Moving()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        // Kiểm tra nếu người chơi đang di chuyển
        if (direction.x != 0 || direction.y != 0)
        {
            // Kiểm tra xem hướng mới có khác với hướng trước đó hay không
            if (direction.x != previousHorizontal || direction.y != previousVertical)
            {
                // Chuyển đổi animation tại đây
                animate.horizontal = direction.x;
                animate.vertical = direction.y;
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

        transform.parent.Translate(direction * moveSpeed * Time.deltaTime);

        //direction *= moveSpeed;
        //rpg2d.velocity = direction;
        

        // Set giá trị của biến isMoving trong script Animate
        animate.isMoving = isMoving;
    }
}
