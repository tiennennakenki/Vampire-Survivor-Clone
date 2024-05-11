using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerCollector : SaiMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected PlayerStats player;
    [SerializeField] protected CircleCollider2D detector;
    [SerializeField] protected float pullSpeed = 300f;

    protected override void Awake()
    {
        base.Awake();
        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }
    protected override void Start()
    {
        player = playerCtrl.Model;
    }

    public void SetRadius(float radius)
    {
        if (!detector) detector = GetComponent<CircleCollider2D>();
        detector.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PickUp pickUp))
        {
            pickUp.Collect(player, pullSpeed);
        }
    }
}
