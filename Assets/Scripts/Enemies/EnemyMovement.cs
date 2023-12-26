using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : SaiMonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] protected Transform player;
    [SerializeField] protected float speed = 1f;

    protected override void Start()
    {
        base.Start();
        this.player = FindObjectOfType<PlayerMovement>().transform;
    }

    protected override void Update()
    {
        base.Update();
        this.MovingFollowTarget();
    }

    protected virtual void MovingFollowTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.speed * Time.deltaTime);  
    }


}
