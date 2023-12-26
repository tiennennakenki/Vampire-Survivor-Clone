using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    public KnifeCtrl knifeCtrl;

    protected override void Start()
    {
        base.Start();
        knifeCtrl = FindObjectOfType<KnifeCtrl>();
    }

    protected override void Update()
    {
        base.Update();
        this.SpawnKnife();
    }

    protected virtual void SpawnKnife()
    {
        transform.position += direction * knifeCtrl.speed * Time.deltaTime;
    }
}
