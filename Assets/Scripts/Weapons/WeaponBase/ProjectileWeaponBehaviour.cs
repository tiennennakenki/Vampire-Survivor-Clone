using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : SaiMonoBehaviour
{
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected float destroyAfterSeconds = 1;

    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, this.destroyAfterSeconds);
    }
}
