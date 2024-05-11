using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SaiMonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance => instance;

    [SerializeField] protected Transform player;
    public Transform Player => player;

    protected override void OnEnable()
    {
        PlayerSelection.CharacterSetEvent += LoadPlayer;
    }

    protected override void OnDisable()
    {
        PlayerSelection.CharacterSetEvent -= LoadPlayer;
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 CameraController allow to exits");
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadPlayer();
    }

    protected override void Update()
    {
        this.FollowPlayer();
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = FindObjectOfType<PlayerCtrl>().transform;

        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    public virtual void FollowPlayer()
    {
        this.transform.position = new Vector2(this.player.position.x, this.player.position.y);
    }
}
