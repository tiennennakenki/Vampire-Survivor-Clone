using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SaiMonoBehaviour
{
    [SerializeField] protected Transform player;
    public Transform Player => player;

    protected override void Update()
    {
        base.Update();
        this.FollowPlayer();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.Find("Player").transform;
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual void FollowPlayer()
    {
        this.transform.position = new Vector2(this.player.position.x, this.player.position.y);
    }
}
